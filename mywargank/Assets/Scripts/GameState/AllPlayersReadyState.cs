using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Util;
using LitJson;
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
            MainController.getInstance().Clear(false);
            //SceneMachineMgr.LoadSceneAsync(SDBattle.GetElement(_data.battleID.ToString()).Scene, LoadSceneFinished);
            SceneMachineMgr.LoadSceneAsync("pathfindNew_00", LoadSceneFinished);
        }

        private void LoadSceneFinished()
        {
            Debug.Log("LoadSceneFinished");
            FightManager.getInstance().Init(_data.battleID.ToString());
            //new FightManager(_data.battleID.ToString());
            RandomHelper.getInstance().InitWithSeed(_data.randomSeed);
            LockStep.LockStepHelper.SetKeyFrameInterVal(_data.keyFrameRange);
            OnKeyFrameMsgHandler.SetClientStartDelayFrame(_data.clientStartDelay);

            for (int i = 0; i < _data.playerInfoList.Count; i++)
            {
                Player player = PlayerMgr.getInstance().InitPlayer(new User(_data.playerInfoList[i].nickname, _data.playerInfoList[i].numberID), _data.playerInfoList[i].seat, _data.playerInfoList[i].portrait, _data.playerInfoList[i].numberID == Accout.numberID, ConvertHelper.ConvertToEnum<TeamNum>(_data.playerInfoList[i].seat));
                player.SetIntergral(_data.playerInfoList[i].integral);
                List<string> cards = JsonMapper.ToObject<List<string>>(_data.playerInfoList[i].cardIds);
                List<string> defaultCards = null;
                if(_data.playerInfoList[i].battleCardGroup != null)
                {
                    defaultCards = JsonMapper.ToObject<List<string>>(_data.playerInfoList[i].battleCardGroup);
                }
                CardDataMgr.getInstance().RecvCardFromServer(_data.playerInfoList[i].numberID, cards, defaultCards);
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
