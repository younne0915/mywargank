using Pomelo.DotNetClient;
using Protocol;
using Simon.CustomSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public class WGPomeloClient
    {
        public PomeloClientType type;
        public PomeloClientState state;
        public PomeloClient client;

        private Dictionary<PomeloClient, int> _dic = new Dictionary<PomeloClient, int>();

        protected string _connectorIP;
        protected int _connectorPort;

        protected string _gateIP;
        protected int _gatePort;

        protected string _versionIP;
        protected int _versionPort;

        protected Action<PomeloResult> _connectServerCallback = null;

        public WGPomeloClient(PomeloClientType type)
        {
            this.type = type;
        }

        public bool ConnectServer(Action<PomeloResult> callback)
        {
            if (_connectServerCallback != null) return false;
            _connectServerCallback = callback;
            if (string.IsNullOrEmpty(_gateIP))
            {
                ConnectVersionServer();
            }
            else if (string.IsNullOrEmpty(_connectorIP))
            {
                ConnectGateServer();
            }
            else
            {
                ConnectConnectorDirect();
            }
            return true;
        }

        public void SetGateServerParam(string gateIP, int gatePort)
        {
            _gateIP = gateIP;
            _gatePort = gatePort;
        }

        public void SerVersionParam(string versionIP, int versionPort)
        {
            _versionIP = versionIP;
            _versionPort = versionPort;
        }

        public void ConnectVersionServer()
        {
            client = new PomeloClient(_versionIP, _versionPort);
            ThreadMgr.RunThread(RunConnectVersionServer);
        }

        public void RunConnectVersionServer()
        {
            SimonSocketResult result = client.ConnectServer();
            if (NetworkHelper.CheckSocketIsConnected(result))
            {
                PomeloMsgRecv.instance.RecevMsg(new OnConnectVersionServerHandler(type));
            }
            else
            {
                PomeloMsgRecv.instance.RecevMsg(new OnConnectServerHandler(type, new PomeloResult((int)PomeloCode.Fail, result.errorMsg)));
            }
        }

        public void ConnectGateServer()
        {
            client = new PomeloClient(_gateIP, _gatePort);
            ThreadMgr.RunThread(RunConnectGateServerThread);
        }

        private void RunConnectGateServerThread()
        {
            SimonSocketResult result = client.ConnectServer();
            if (NetworkHelper.CheckSocketIsConnected(result))
            {
                PomeloMsgRecv.instance.RecevMsg(new OnConnectGateBackHandler(type));
            }
            else
            {
                PomeloMsgRecv.instance.RecevMsg(new OnConnectServerHandler(type, new PomeloResult((int)PomeloCode.Fail, result.errorMsg)));
            }
        }

        public virtual void GetConnectorIPFromGate()
        {
        }

        public virtual void GetGateIPFromVersion()
        {
        }

        public void ConnectConnectorDirect()
        {
            client = new PomeloClient(_connectorIP, _connectorPort);//fight event
            ThreadMgr.RunThread(RunConnectConnectorServerThread);
        }

        public void ConnectConnectorWithIP()
        {
            DisConnect("gate server disconnect");
            ConnectConnectorDirect();
        }

        private void RunConnectConnectorServerThread()
        {
            if (!_dic.ContainsKey(client))
            {
                _dic.Add(client, 1);
            }
            SimonSocketResult result = client.ConnectServer();
            if (NetworkHelper.CheckSocketIsConnected(result))
            {
                RegisterPushHandler();
                PomeloMsgRecv.instance.RecevMsg(new OnConnectServerHandler(type, new PomeloResult((int)PomeloCode.Success, "")));
            }
            else
            {
                PomeloMsgRecv.instance.RecevMsg(new OnConnectServerHandler(type, new PomeloResult((int)PomeloCode.Fail, result.errorMsg)));
            }
        }

        public virtual void RegisterPushHandler()
        {
        }

        public void Request(string route, SimonMsg msg, Action<object> action)
        {
            client.Request(route, msg, action);
        }

        public void DisConnect(string reason)
        {
            client.Disconnect(reason);
        }

        public void DisConnectByPomelo(string reason)
        {
            if (client != null)
            {
                StateMachineController.instance.OnDisconnect(type, reason);
            }
        }

        public void ExecuteConnectServerCalback(PomeloResult result)
        {
            if (NetworkHelper.CheckPomeloCodeIsSuccess(result.code))
            {
                state = PomeloClientState.Connected;
            }
            else
            {
                state = PomeloClientState.Disconnected;
            }
            if (_connectServerCallback != null)
            {
                _connectServerCallback(result);
                _connectServerCallback = null;
            }
        }

        public void ClearConnectorIP()
        {
            _connectorIP = string.Empty;
            _connectorPort = 0;
        }
    }
}
