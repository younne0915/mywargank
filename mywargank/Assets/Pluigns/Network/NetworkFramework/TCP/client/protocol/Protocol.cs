using LitJson;
using Protocol;
using Simon.CustomSocket;
using System;
using System.Net.Sockets;
using System.Text;

namespace Pomelo.DotNetClient
{
    public class Protocol
    {
        private MessageProtocol messageProtocol;
        private ProtocolState state;
        private Transporter transporter;
        private HandShakeService handshake;
        private HeartBeatService heartBeatService = null;
        private PomeloClient pc;
        private Action<SimonSocketResult> protocolStartCallback;

        public PomeloClient getPomeloClient()
        {
            return this.pc;
        }

        public Protocol(PomeloClient pc, Socket socket)
        {
            this.pc = pc;
            this.transporter = new Transporter(socket, this.processMessage);
            this.handshake = new HandShakeService(this);
            this.state = ProtocolState.start;
        }

        public void start(Action<SimonSocketResult> callback)
        {
            this.protocolStartCallback = callback;
            this.transporter.start(this.onTransportDisconnect, this.onTransportSendError, this.onTransportReceiveError);
            this.handshake.request(null, data =>
            {
                SimonSocketResult result = new SimonSocketResult(SocketError.Success);
                this.protocolStartCallback.Invoke(result);
            });

            this.state = ProtocolState.handshaking;
        }

        private void onTransportDisconnect()
        {
            this.pc.Disconnect("transporter closed");
        }

        private void onTransportSendError(Exception e)
        {
            this.pc.Disconnect(e.Message);
        }

        private void onTransportReceiveError(Exception e)
        {
            if (this.state == ProtocolState.handshaking)
            {
                if (e is SocketException)
                {
                    SocketException se = (SocketException)e;
                    SimonSocketResult psResult = new SimonSocketResult(se.SocketErrorCode, se.Message);
                    this.protocolStartCallback.Invoke(psResult);
                    return;
                }
            }

            this.pc.Disconnect(e.Message);
        }

        public void stop()
        {
            transporter.stop();

            if (heartBeatService != null) heartBeatService.stop();

            this.state = ProtocolState.closed;
        }

        //Send notify, do not need id
        public void sendMsg(string route, JsonData msg)
        {
            this.sendMsg(route, 0, msg);
        }

        /// <summary>
        /// send function
        /// </summary>
        /// <param name="route"></param>
        /// <param name="id">option, notify is 0</param>
        /// <param name="msg"></param>

        public void sendMsg(string route, uint id, JsonData msg)
        {
            if (this.state != ProtocolState.working) return;

            byte[] body = messageProtocol.encode(route, id, msg);

            sendPackage(PackageType.PKG_DATA, body);
        }

        public void sendPackageWithoutContent(PackageType type)
        {
            if (this.state == ProtocolState.closed) return;
            transporter.send(PackageProtocol.encode(type));
        }

        //Send message use the transporter
        public void sendPackage(PackageType type, byte[] body)
        {
            if (this.state == ProtocolState.closed) return;

            byte[] pkg = PackageProtocol.encode(type, body);

            transporter.send(pkg);
        }

        //Invoke by Transporter, process the message
        private void processMessage(byte[] bytes)
        {
            Package pkg = PackageProtocol.decode(bytes);

            //Ignore all the message except handshading at handshake stage
            if (pkg.type == PackageType.PKG_HANDSHAKE && this.state == ProtocolState.handshaking)
            {

                //Ignore all the message except handshading
                string handshakeContent = Encoding.UTF8.GetString(pkg.body);
                JsonData data;
                if (EncryptionConfig.USE_ENCRYPT_PROTOCOL)
                {
                    string clearContent = AES128Helper.Decrypt(handshakeContent, EncryptionConfig.GUESS_KEY);
                    data = JsonMapper.ToObject(clearContent);
                }
                else
                {
                    data = JsonMapper.ToObject(handshakeContent);
                }

                processHandshakeData(data);

                this.state = ProtocolState.working;

            }
            else if (pkg.type == PackageType.PKG_HEARTBEAT && this.state == ProtocolState.working)
            {
                this.heartBeatService.resetTimeout();
            }
            else if (pkg.type == PackageType.PKG_DATA && this.state == ProtocolState.working)
            {
                this.heartBeatService.resetTimeout();
                pc.processMessage(messageProtocol.decode(pkg.body));
            }
            else if (pkg.type == PackageType.PKG_KICK)
            {
                string kickReason = Encoding.UTF8.GetString(pkg.body);
                Util.WGLogger.Log(Util.LogModule.Debug, "kickReason " + kickReason);
                this.pc.Disconnect(JsonMapper.ToObject(kickReason));
            }
        }

        private void processHandshakeData(JsonData msg)
        {
            //Handshake error
            if (!msg.ContainsKey("code") || !msg.ContainsKey("sys") || Convert.ToInt32(msg["code"].ToJson()) != 200)
            {
                throw new Exception("Handshake error! Please check your handshake config.");
            }

            //Set compress data
            JsonData sys = msg["sys"];

            JsonData dict = new JsonData();
            if (sys.ContainsKey("dict")) dict = sys["dict"];

            JsonData protos = new JsonData();
            JsonData serverProtos = new JsonData();
            JsonData clientProtos = new JsonData();

            if (sys.ContainsKey("protos"))
            {
                protos = sys["protos"];
                serverProtos = protos["server"];
                clientProtos = protos["client"];
            }

            messageProtocol = new MessageProtocol(dict, serverProtos, clientProtos);

            //Init heartbeat service
            int interval = 0;
            if (sys.ContainsKey("heartbeat")) interval = Convert.ToInt32(sys["heartbeat"].ToJson());
            heartBeatService = new HeartBeatService(interval, this);

            if (interval > 0)
            {
                heartBeatService.start();
            }

            //send ack and change protocol state
            handshake.ack();
            this.state = ProtocolState.working;

            //Invoke handshake callback
            JsonData user = new JsonData();
            user.SetJsonType(JsonType.Object);
            if (msg.ContainsKey("user")) user = msg["user"];
            handshake.invokeCallback(user);
        }
    }
}

