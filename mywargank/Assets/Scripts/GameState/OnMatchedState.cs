using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using FightResponseHandler = Protocol.ServerResponseInterFace.Fight.FightHandler;

namespace WG
{
    public class OnMatchedState : GameState
    {
        public OnMatchedState(GameStateType type) : base(type)
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();
            PomeloMsgSender.SendReady(SendReadyCallback);
        }

        private void SendReadyCallback(FightResponseHandler.Ready response)
        {
            if (NetworkHelper.CheckPomeloCodeIsSuccess(response.code))
            {
            }
            else
            {

            }
        }

        protected override void OnFightDisconnect(string errorMsg)
        {

        }
    }
}
