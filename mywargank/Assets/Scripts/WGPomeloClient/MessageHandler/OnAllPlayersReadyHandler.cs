using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocol;
using UnityEngine;

namespace WG
{
    public class OnAllPlayersReadyHandler : IMsgHandler
    {
        public static string InterfaceName = ServerPushInterface.OnAllPlayersReady.InterfaceName;

        private ServerPushInterface.OnAllPlayersReady _result;

        public OnAllPlayersReadyHandler(object result)
        {
            _result = (ServerPushInterface.OnAllPlayersReady)result;
        }

        public void ExcuteMsg()
        {
            AllPlayersReadyState.SetAllPlayersReadyMsg(_result);
            StateMachineController.instance.SetNextState(GameStateType.AllPlayersReady);
        }
    }
}
