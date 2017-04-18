using Simon.CustomSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WG
{
    public class LoginState : GameState
    {
        public LoginState(GameStateType type) : base(type)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            PomeloStatusMgr.ClearConnectorIP();
            PomeloStatusMgr.ConnectServer(PomeloClientType.PeaceServer, ConnectServerCallback);
        }

        private void ConnectServerCallback(PomeloResult result)
        {
            if (NetworkHelper.CheckPomeloResultIsSuccess(result))
            {
                LoginController.getInstance().OpenLoginUI();
            }
            else
            {

            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }
    }
}
