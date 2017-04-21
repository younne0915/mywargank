using LockStep;
using PathFind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Util;

namespace WG
{
    public enum BattleResult
    {
        Lose = 0,
        Win,
        Draw,
        PerfectWin,
        PerfectLose,
        Default,
    }

    public enum GAME_MODE_CLIENT
    {
        ONE_VERSUS_ONE = 1,
        FRIEND_MATCH_ONE = 2,
        THREE_VERSUS_THREE = 3
    }

    public enum BattleType
    {
        oneVersusOne,
        climbtower,
        guide,
        singlePlayer,
        friendMatch,
    }

    public struct Command
    {
        public int executeFrame;
        public int type;
        public List<int> paramsList;
        public int numberID;

        public Command(int executeFrame, int type, List<int> param, int numberID)
        {
            this.executeFrame = executeFrame;
            this.type = type;
            this.paramsList = param;
            this.numberID = numberID;
        }
    }



    public class FightManager : Singleton<FightManager>
    {

        private SDBattle _sdBattle;
        public SDBattle sdBattle
        {
            get { return _sdBattle; }
        }

        private BattleType _battleType;

        private bool _startLockStep = false;
        public bool startLockStep
        {
            get { return _startLockStep; }
        }

        public void Init(string battleID)
        {
            SetBattleData(battleID);
            LoadPath();
            LockStepEngine.Init();
        }

        private void SetBattleData(string battleID)
        {
            _sdBattle = SDBattle.GetElement(battleID);
            _battleType = ConvertHelper.ConvertToEnum<BattleType>(_sdBattle.BattleType);
        }

        private void LoadPath()
        {
            string path = AstarUtil.PATHDATA_PATH + _sdBattle.ScenePath;
            TextAsset file = WGLoader.LoadRes<TextAsset>(path, ".txt");
            string[] lines = file.text.Split('\n');
            AstarUtil.LoadMap(lines);
            SubStrateMgr.LoadSubStrateModel();
        }

        public void BeganLockStepMgr()
        {
            _startLockStep = true;
            LockStepMgr.getInstance().BeganLockStep();
        }

        private void Clear()
        {
            //TODO

        }

        public void GameOver(BattleResult result)
        {

        }
    }
}
