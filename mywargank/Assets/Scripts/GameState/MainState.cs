using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WG
{
    public class MainState : GameState
    {
        public MainState(GameStateType type) : base(type)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            PomeloStatusMgr.DisconnectServer(PomeloClientType.FightServer, "Init Disconnect Fight Server");
            SceneMachineMgr.LoadSceneAsync("Main", LoadMainCallback);
        }

        public void LoadMainCallback()
        {
            if(MainController.instance == null)
            {
                new MainController();
            }
            MainController.instance.OpenMainUI();
            if(LoginController.instance != null)
            {
                LoginController.instance.Clear(true);
            }
        }

        protected override void OnFightDisconnect(string errorMsg)
        {
            base.OnFightDisconnect(errorMsg);
        }
    }
}
