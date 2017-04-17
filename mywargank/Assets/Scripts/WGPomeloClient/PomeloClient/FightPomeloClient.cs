using Pomelo.DotNetClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GateResponseHandler = Protocol.ServerResponseInterFace.Gate.GateHandler;

namespace WG
{
    public class FightPomeloClient : WGPomeloClient
    {
        public FightPomeloClient(PomeloClientType type): base(type)
        {

        }

        public override void GetConnectorIPFromGate()
        {
            PomeloMsgSender.GetFightConnectorIP(Accout.numberID.ToString(), GetConnectorIPFromGateCallback);
        }

        public void GetConnectorIPFromGateCallback(GateResponseHandler.GetGameConnector response)
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
            //disconnect 不是在上一个client注册的么，为什么还会监听到，每次都创建新的client,岂不是要执行很多遍disconnect？？？
            client.On(PomeloClient.EVENT_DISCONNECT, (data)=> 
            {
                PomeloMsgRecv.instance.RecevMsg(new OnDisconnectHandler(PomeloClientType.FightServer, data));
            });

            client.On (OnMatchedMsgHandler.InterfaceName, (data) =>
			{
				PomeloMsgRecv.instance.RecevMsg(new OnMatchedMsgHandler(data));
			});

            client.On (OnAllPlayersReadyHandler.InterfaceName, (data) =>
			{
				PomeloMsgRecv.instance.RecevMsg(new OnAllPlayersReadyHandler(data));
			});

            client.On(OnKeyFrameMsgHandler.interfaceName, (data)=> 
            {
                PomeloMsgRecv.instance.RecevMsg(new OnKeyFrameMsgHandler(data));
            });
        }
    }
}
