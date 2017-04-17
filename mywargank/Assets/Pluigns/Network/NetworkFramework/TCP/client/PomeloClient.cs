using LitJson;
using Protocol;
using Simon.CustomSocket;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Pomelo.DotNetClient
{
    public class PomeloClient
    {
        public const string EVENT_DISCONNECT = "disconnect";
      
        private EventManager eventManager;
        private Socket socket;
        private Protocol protocol;
        private uint reqId = 1;
        private string host;
        private int port;
        private ManualResetEvent connectTimeoutFlagEvent = new ManualResetEvent(false);
        private SimonSocketResult connectResult;
        private Dictionary<string, Action<object>> onReceivePushCallbackDic;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="host">host string</param>
        /// <param name="port">port int</param>
        /// <param name="receiveTimeout">receive timeout, in seconds. 5 is by defualt</param>
        /// <param name="sendTimeout">send timeout, in seconds. 5 is by defualt</param>
        public PomeloClient(string host, int port)
        {
            this.host = host;
            this.port = port;
            this.eventManager = new EventManager();
            this.onReceivePushCallbackDic = new Dictionary<string, Action<object>>();
        }

        /// <summary>
        /// sync connect server.
        /// </summary>
        /// <param name="timeout">receive timeout, in millisecond. 3000 is by defualt</param>
        /// <returns>SimonSocketResult</returns>
        /// 

        public SimonSocketResult ConnectServer(int timeout = 3000)
        {
		//	string ip = this.host;

            this.connectTimeoutFlagEvent.Reset();
			IPAddress ipAdress = null;
            if (this.host.Contains(".com"))
            {
                try
                {
                    IPHostEntry hostinfo = Dns.GetHostEntry(this.host);
                    IPAddress[] aryIP = hostinfo.AddressList;
                    string ip = aryIP[0].ToString();
                    ipAdress = IPAddress.Parse(ip);
                }
                catch(Exception e)
                {
                    Util.WGLogger.Log(Util.LogModule.Debug, " e " + e.ToString());                  
                }
            }
            else
            {
                ipAdress = IPAddress.Parse(this.host);
            }
			//if (IPAddress.TryParse(this.host, out ipAdress))
			//{

			//}
			//else
			//{
		
			//}      
		 	//	IPAddress ipAdress = IPAddress.Parse (ip);
			if (ipAdress == null)
				return new SimonSocketResult (SocketError.TimedOut,"can not parse dns " + this.host);
			IPEndPoint ipEndPoint = new IPEndPoint(ipAdress, this.port);      
            this.socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.socket.BeginConnect(ipEndPoint, BeginConnectCallback, this.socket);

            if (this.connectTimeoutFlagEvent.WaitOne(timeout))
            {
                Util.WGLogger.Log(Util.LogModule.Debug, " connectResult " + connectResult.result);
                //if (this.connectResult.result != SocketError.Success) 
                //{
                //	return ConnectServerIPV6 (timeout);
                //}

                return this.connectResult;
            }
            else
            {
                this.Disconnect("connected pomelo time out!");
                //return new SimonSocketResult(SocketError.TimedOut, "Connected time is longer than the time which you set!");
                return new SimonSocketResult(SocketError.TimedOut, "IDS_Connected_Timeout");
            }
        }



		public SimonSocketResult ConnectServerIPV6(int timeout = 3000)
		{
			string ip = ConvertIpv4ToV6(this.host);

			this.connectTimeoutFlagEvent.Reset();
			IPAddress ipAdress = IPAddress.Parse (ip);
			IPEndPoint ipEndPoint = new IPEndPoint(ipAdress, this.port);            
			this.socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			this.socket.BeginConnect(ipEndPoint, BeginConnectCallback, this.socket);
			if (this.connectTimeoutFlagEvent.WaitOne(timeout))
			{
				return this.connectResult;	
			}
			else
			{
				this.Disconnect("connected pomelo time out!");
                //return new SimonSocketResult(SocketError.TimedOut, "Connected time is longer than the time which you set!");
                return new SimonSocketResult(SocketError.TimedOut, "IDS_Connected_Timeout");
            }
		}

		public string ConvertIpv4ToV6(string ip)
		{			
			string retIP = "64:ff9b::";
			string[] nums = ip.Split ('.');

			for (int i = 0; i < nums.Length; i++) {
				string hex = int.Parse (nums [i]).ToString ("X2");
				retIP += hex;
				if (i % 2 == 1 && i != 3) 
				{
					retIP += ":";
				}
			}
			UnityEngine.Debug.Log ("ret ip is " + retIP);
			return retIP;
		}
			
        /// <summary>
        /// async connect server
        /// </summary>
        /// <param name="callback">callback</param>
        /// <param name="protocolTimeout">protocol started time out, total time includes send, receive, or handshake.</param>
        public void ConnectServerAsync(Action<SimonSocketResult> callback, int protocolTimeout = 3000)
        {
            this.connectTimeoutFlagEvent.Reset();

            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(this.host), this.port);
            this.socket.BeginConnect(ipEndPoint, ar =>
            {
                BeginConnectCallback(ar);

                bool getResultInTime = this.connectTimeoutFlagEvent.WaitOne(protocolTimeout);
                if (getResultInTime)
                {
                    callback.Invoke(this.connectResult);
                }
                else
                {
                    this.Disconnect("connected pomelo time out!");
                    callback.Invoke(new SimonSocketResult(SocketError.TimedOut, "Connected time is longer than the time which you set!"));
                }
            }, this.socket);
        }

        private void BeginConnectCallback(IAsyncResult ar)
        {
            try
            {
                this.protocol = new Protocol(this, this.socket);
                this.socket.EndConnect(ar);
                if (this.socket.Connected)
                {
                    this.protocol.start((result) =>
                    {
                        this.SetConnectResult(result);
                    });
                }
                else
                {
                    this.SetConnectResult(new SimonSocketResult(SocketError.SocketError, "Shit, socket did not connect!"));
                }
            }
            catch (SocketException ex)
            {
                this.SetConnectResult(new SimonSocketResult(ex.SocketErrorCode, ex.Message));
            }
        }

        private void SetConnectResult(SimonSocketResult res)
        {
            this.connectResult = res;
            this.connectTimeoutFlagEvent.Set();
        }

        public void Notify(string route, SimonMsg msg)
        {
            JsonData msgJD = msg.GetJD();
            Console.WriteLine(msgJD.ToJson());
            this.notify(route, msgJD);
        }

        public void Request(string route, SimonMsg msg, Action<object> action)
        {
            JsonData msgJD = msg.GetJD();

            this.request(route, msgJD, data =>
            {
                string jsonString = string.Empty;
                if (data == null)
                {
                    action.Invoke(null);
                }
                else
                {
                    if (EncryptionConfig.USE_ENCRYPT_RESPONSE)
                    {
                        string encryStr = data["msg"].ToJson();
                        jsonString = AES128Helper.Decrypt(encryStr, EncryptionConfig.GUESS_KEY);

                    }
                    else
                    {
                        jsonString = data.ToJson();
                    }

                    string typeFullName = ClassFullName.GetClassFullName(route);
                    Type type = Type.GetType(typeFullName, true, true);
                    object response = JsonMapper.ToObject(type, jsonString);
                    action.Invoke(response);
                }
            });
        }


        public void On(string eventName, Action<object> action)
        {
            if (!this.onReceivePushCallbackDic.ContainsKey(eventName))
            {
                this.onReceivePushCallbackDic.Add(eventName, action);
            }
            else
            {
                Util.WGLogger.LogError(Util.LogModule.Pomelo, "Repeat register pomelo action " + eventName);
                this.onReceivePushCallbackDic[eventName] = action;
            }
            this.eventManager.AddOnEvent(eventName, HandleOnEventTrigger);
        }

        private void HandleOnEventTrigger(string eventName, JsonData data)
        {
            if (eventName == EVENT_DISCONNECT)
            {
                this.onReceivePushCallbackDic[eventName].Invoke(data["reason"].ToJson());
                return;
            }

            if (data == null)
            {
                this.onReceivePushCallbackDic[eventName].Invoke("");
                return;
            }
            else
            {
                string typeFullName = ClassFullName.GetClassFullName(eventName);
                if (EncryptionConfig.USE_ENCRYPT_PUSH)
                {
                    if (EncryptionConfig.PUSH_ENCRYPT_INFO.ContainsKey(typeFullName))
                    {
                        string[] enfieldNames = EncryptionConfig.PUSH_ENCRYPT_INFO[typeFullName];
                        foreach (string enfiledName in enfieldNames)
                        {

                            if (enfiledName.IndexOf('.') > -1)
                            {
                                string[] names = enfiledName.Split('.');
                                string parentName = names[0];
                                if (data.ContainsKey(parentName) && data[parentName] != null)
                                {
                                    int arrayLength = data[parentName].Count;
                                    for (var j = 0; j < arrayLength; j++)
                                    {
                                        string childName = names[1];
                                        string encryptData = data[parentName][j][childName].ToJson();
                                        string clearData = AES128Helper.Decrypt(encryptData, EncryptionConfig.GUESS_KEY);
                                        data[parentName][j][childName] = clearData;
                                    }
                                }
                            }
                            else
                            {
                                string encryptValue = data[enfiledName].ToJson();
                                string clearValue = AES128Helper.Decrypt(encryptValue, EncryptionConfig.GUESS_KEY);
                                data[enfiledName] = clearValue;
                            }
                        }
                    }
                }

                string jsonString = data.ToJson();
                Type type = Type.GetType(typeFullName, true, true);

                object response = JsonMapper.ToObject(type, jsonString);
                this.onReceivePushCallbackDic[eventName].Invoke(response);
            }
        }

        private void request(string route, JsonData msg, Action<JsonData> action)
        {
            this.eventManager.AddCallBack(reqId, action);
            protocol.sendMsg(route, reqId, msg);

            reqId++;
        }

        private void notify(string route, JsonData msg)
        {
            protocol.sendMsg(route, msg);
        }

        internal void processMessage(Message msg)
        {
            if (msg.type == MessageType.MSG_RESPONSE)
            {
                eventManager.InvokeCallBack(msg.id, msg.data);
            }
            else if (msg.type == MessageType.MSG_PUSH)
            {
                eventManager.InvokeOnEvent(msg.route, msg.data);
            }
        }

        public void Disconnect(string reason)
        {
            JsonData reasonJD = new JsonData();
            reasonJD["reason"] = reason;
            this.CloseAll(reasonJD);
        }

        public void Disconnect(JsonData reasonJD)
        {
            this.CloseAll(reasonJD);
        }

        private void CloseAll(JsonData disconnectReason)
        {
            try
            {
                this.protocol.stop();
                this.socket.Shutdown(SocketShutdown.Both);
                this.socket.Close();
            }
            catch (Exception e)
            {
				if (!disconnectReason.ContainsKey("reason")) {
					disconnectReason ["reason"] = e.Message;
				}
            }
            finally
            {
                eventManager.InvokeOnEvent(EVENT_DISCONNECT, disconnectReason);
            }
        }
    }
}

