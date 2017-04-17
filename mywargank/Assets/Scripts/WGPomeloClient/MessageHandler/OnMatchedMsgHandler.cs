using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protocol;

namespace WG{
	public class OnMatchedMsgHandler: IMsgHandler {

        public static string InterfaceName = ServerPushInterface.OnMatched.InterfaceName;

        private ServerPushInterface.OnMatched _matchedData;

        public OnMatchedMsgHandler(object json)
		{
            _matchedData = (ServerPushInterface.OnMatched)json;
        }

		public void ExcuteMsg()
		{
            StateMachineController.instance.SetNextState(GameStateType.OnMatched);
		}
	}
}
