﻿using Simon.CustomSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Util;
using EntryResponseHandler = Protocol.ServerResponseInterFace.GameConnector.EntryHandler;

namespace WG
{
    public class MatchState : GameState
    {
        public MatchState(GameStateType type) : base(type)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            PomeloStatusMgr.ConnectServer(PomeloClientType.FightServer, OnConnectFightServerCallback);
        }

        private void OnConnectFightServerCallback(PomeloResult result)
        {
            if (NetworkHelper.CheckPomeloResultIsSuccess(result))
            {
                PomeloMsgSender.SendEnterMatchQueue(Accout.numberID, (int)GAME_MODE_CLIENT.ONE_VERSUS_ONE, SendEnterMatchQueueCallback);
            }
            else
            {
                PomeloFailed(result.errorMsg);
            }
        }

        public void SendEnterMatchQueueCallback(EntryResponseHandler.EnterMatchQueue response)
        {
            if (NetworkHelper.CheckPomeloCodeIsSuccess(response.code))
            {
                MatchUIController.getInstance().SetEmtime(response.emtime);
                MatchUIController.getInstance().OpenMatchUI(MainUIController.getInstance().mainUI);
            }
            else
            {
                PomeloFailed(response.errorMsg);
            }
        }

        protected override void OnFightDisconnect(string errorMsg)
        {
            base.OnFightDisconnect(errorMsg);

            DisconnectError error = ConvertHelper.ConvertToEnum<DisconnectError>(errorMsg);

            switch (error)
            {
                case DisconnectError.CancelMatch:
                    MatchUIController.getInstance().HideMatchUI();
                    StateMachineController.getInstance().SetNextState(GameStateType.Main);
                    break;
                default:
                    StateMachineController.getInstance().SetNextState(GameStateType.Main);
                    break;
            }
			

        }
    }
}
