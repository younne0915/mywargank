using Pomelo.DotNetClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Util;
using GateResponseHandler = Protocol.ServerResponseInterFace.Gate.GateHandler;

namespace WG
{
    public class PeacePomeloClient : WGPomeloClient
    {
        public PeacePomeloClient(PomeloClientType type) : base(type)
        {

        }

        public override void GetConnectorIPFromGate()
        {
            PomeloMsgSender.GetPeaceConnectorIP(Constant.REGION, GetConnectorIPFromGateCallback);
        }

        public override void GetGateIPFromVersion()
        {
            PomeloMsgSender.GetGateIP(Constant.APP_VERSION, GetGateIPFromVersionCallback);
        }

        public void GetGateIPFromVersionCallback(Protocol.ServerResponseInterFace.Version.VersionHandler.GetGateServer response)
        {
            if (NetworkHelper.CheckPomeloCodeIsSuccess(response.code))
            {
                PomeloStatusMgr.SetGateServerParam(response.host, response.port);
                WriteGateServetToLocal();
                WGLogger.Log(LogModule.Debug, "gate ip is " + _gateIP + " port " + _gatePort);
                ConnectGateServer();
            }
            else
            {
                PomeloMsgRecv.instance.RecevMsg(new OnConnectServerHandler(PomeloClientType.PeaceServer, new PomeloResult((int)PomeloCode.Fail, response.errorMsg)));
            }
        }

        void WriteGateServetToLocal()
        {
            if (!string.IsNullOrEmpty(_gateIP))
            {
                string path = FileHelper.GetPath(Constant.GATE_FILE_NAME);
                GateFile file = new GateFile();
                file.ip = _gateIP;
                file.port = _gatePort;
                file.time = System.DateTime.Now;
                string content = LitJson.JsonMapper.ToJson(file);
                FileHelper.WriteFile(content, path);
            }
        }

        public void GetConnectorIPFromGateCallback(GateResponseHandler.GetSceneConnector response)
        {
            if (NetworkHelper.CheckPomeloCodeIsSuccess(response.code))
            {
                _connectorIP = response.host;
                _connectorPort = response.port;
                ConnectConnectorWithIP();
            }
            else
            {
                Debug.Log("pomelo return not success");
            }
        }

        public override void RegisterPushHandler()
        {
            client.On(PomeloClient.EVENT_DISCONNECT, (response) =>
            {
                PomeloMsgRecv.instance.RecevMsg(new OnDisconnectHandler(PomeloClientType.PeaceServer, PomeloClient.EVENT_DISCONNECT));
            });
        }
    }
}
