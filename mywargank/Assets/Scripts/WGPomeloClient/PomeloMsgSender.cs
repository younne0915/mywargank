using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GateRequestHandler = Protocol.ServerInterFace.Gate.GateHandler;
using GateResponseHandler = Protocol.ServerResponseInterFace.Gate.GateHandler;


using UserRequestHandler = Protocol.ServerInterFace.SceneConnector.UserHandler;
using UserReqsponseHandler = Protocol.ServerResponseInterFace.SceneConnector.UserHandler;

using EntryRequestHandler = Protocol.ServerInterFace.GameConnector.EntryHandler;
using EntryResponseHandler = Protocol.ServerResponseInterFace.GameConnector.EntryHandler;

using MatchRequestHandler = Protocol.ServerInterFace.Match.MatchHandler;
using MatchResponseHandler = Protocol.ServerResponseInterFace.Match.MatchHandler;

using FightRequestHandler = Protocol.ServerInterFace.Fight.FightHandler;
using FightResponseHandler = Protocol.ServerResponseInterFace.Fight.FightHandler;

using VersionServerRequestHandler = Protocol.ServerInterFace.Version.VersionHandler.GetGateServer;
using VersionServerResponseHandler = Protocol.ServerResponseInterFace.Version.VersionHandler.GetGateServer;


namespace WG
{
    public class PomeloMsgSender
    {
        public static void GetGateIP(string appVersion, Action<VersionServerResponseHandler> callback)
        {
            VersionServerRequestHandler request = new VersionServerRequestHandler(appVersion);
            PomeloStatusMgr.SendMsg(PomeloClientType.PeaceServer, VersionServerRequestHandler.InterfaceName, request, (response) =>
            {
                PomeloMsgRecv.getInstance().RecevMsg(new OnCommonMsgHandler<VersionServerResponseHandler>(response, callback));
            });
        }

        public static void GetPeaceConnectorIP(string region, Action<GateResponseHandler.GetSceneConnector> callback)
        {
            GateRequestHandler.GetSceneConnector request = new GateRequestHandler.GetSceneConnector(region);
            PomeloStatusMgr.SendMsg(PomeloClientType.PeaceServer, GateRequestHandler.GetSceneConnector.InterfaceName, request, (response)=> 
            {
                PomeloMsgRecv.getInstance().RecevMsg(new OnCommonMsgHandler<GateResponseHandler.GetSceneConnector>(response, callback));
            });
        }

        public static void GetFightConnectorIP(string numberID, Action<GateResponseHandler.GetGameConnector> callback)
        {
            GateRequestHandler.GetGameConnector request = new GateRequestHandler.GetGameConnector(numberID);
            PomeloStatusMgr.SendMsg(PomeloClientType.FightServer, GateRequestHandler.GetGameConnector.InterfaceName, request, (response) =>
            {
                PomeloMsgRecv.getInstance().RecevMsg(new OnCommonMsgHandler<GateResponseHandler.GetGameConnector>(response, callback));
            });
        }

        public static void SendLogin(string username, string password, string platform, Action<UserReqsponseHandler.Login> callback)
        {
            UserRequestHandler.Login request = new UserRequestHandler.Login(username, password, platform);
            PomeloStatusMgr.SendMsg(PomeloClientType.PeaceServer, UserRequestHandler.Login.InterfaceName, request, (response) =>
            {
                PomeloMsgRecv.getInstance().RecevMsg(new OnCommonMsgHandler<UserReqsponseHandler.Login>(response, callback));
            });
        }

        public static void SendEnterMatchQueue(int numberID, int gameMode, Action<EntryResponseHandler.EnterMatchQueue> callback)
        {
            EntryRequestHandler.EnterMatchQueue request = new EntryRequestHandler.EnterMatchQueue(numberID, gameMode);
            PomeloStatusMgr.SendMsg(PomeloClientType.FightServer, EntryRequestHandler.EnterMatchQueue.InterfaceName, request, (response) =>
            {
                PomeloMsgRecv.getInstance().RecevMsg(new OnCommonMsgHandler<EntryResponseHandler.EnterMatchQueue>(response, callback));
            });
        }

        public static void SendCancleMatch(Action<MatchResponseHandler.CancelMatch> callback)
        {
            MatchRequestHandler.CancelMatch request = new MatchRequestHandler.CancelMatch();
            PomeloStatusMgr.SendMsg(PomeloClientType.FightServer, MatchRequestHandler.CancelMatch.InterfaceName, request, (response)=> 
            {
                PomeloMsgRecv.getInstance().RecevMsg(new OnCommonMsgHandler<MatchResponseHandler.CancelMatch>(response, callback));
            });
        }

        public static void SendReady(Action<FightResponseHandler.Ready> callback)
        {
            FightRequestHandler.Ready request = new FightRequestHandler.Ready();
            PomeloStatusMgr.SendMsg(PomeloClientType.FightServer, FightRequestHandler.Ready.InterfaceName, request, (response) =>
            {
                PomeloMsgRecv.getInstance().RecevMsg(new OnCommonMsgHandler<FightResponseHandler.Ready>(response, callback));
            });
        }

        public static void SendStartFight(Action<FightResponseHandler.StartFight> callback)
        {
            FightRequestHandler.StartFight request = new FightRequestHandler.StartFight();
            PomeloStatusMgr.SendMsg(PomeloClientType.FightServer, FightRequestHandler.StartFight.InterfaceName, request, (response)=> 
            {
                PomeloMsgRecv.getInstance().RecevMsg(new OnCommonMsgHandler<FightResponseHandler.StartFight>(response,callback));
            });
        }

        public static void SendBattleCommand(int expectExcuteLogicFrame, int type, List<int> parmas)
        {
            FightRequestHandler.Fight request = new FightRequestHandler.Fight(expectExcuteLogicFrame, type, parmas);
            PomeloStatusMgr.SendMsg(PomeloClientType.FightServer, FightRequestHandler.Fight.InterfaceName, request, null);
        }
    }
}
