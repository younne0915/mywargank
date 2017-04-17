using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using FightHandler = Protocol.ServerResponseInterFace.Fight.FightHandler;

namespace WG
{
    public class AllPlayersReadyState : GameState
    {
        private static Protocol.ServerPushInterface.OnAllPlayersReady _data;

        public AllPlayersReadyState(GameStateType type): base(type)
        {

        }

        public static void SetAllPlayersReadyMsg(Protocol.ServerPushInterface.OnAllPlayersReady data)
        {
            _data = data;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            MainController.instance.Clear(false);
            //SceneMachineMgr.LoadSceneAsync(SDBattle.GetElement(_data.battleID.ToString()).Scene, LoadSceneFinished);
            SceneMachineMgr.LoadSceneAsync("pathfindNew_00", LoadSceneFinished);
        }

        private void LoadSceneFinished()
        {
            Debug.Log("LoadSceneFinished");
            new FightManager(_data.battleID.ToString());
            RandomHelper.instance.InitWithSeed(_data.randomSeed);
            LockStep.LockStepHelper.SetKeyFrameInterVal(_data.keyFrameRange);
            OnKeyFrameMsgHandler.SetClientStartDelayFrame(_data.clientStartDelay);
            for (int i = 0; i < _data.playerInfoList.Count; i++)
            {
                //Player player = PlayerMgr.instance.
            }

            //TODO
            GameObject go = new GameObject("Mgr");
            go.AddComponent<GameMgr>();

            PomeloMsgSender.SendStartFight(OnSendStartFightCallback);
        }

        private void OnSendStartFightCallback(FightHandler.StartFight response)
        {
            if (NetworkHelper.CheckPomeloCodeIsSuccess(response.code))
            {

            }
            else
            {
                //TODO
            }
        }
    }
}
