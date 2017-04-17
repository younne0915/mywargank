using PathFind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Util;

namespace WG
{
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

    public class FightManager
    {
        public static FightManager instance = null;

        private SDBattle _battle;
        private BattleType _battleType;

        public FightManager(string battleID)
        {
            if(instance != null)
            {
                instance.Clear();
            }
            new SubStrateMgr();
            new PlayerMgr();
            new CardDataMgr();
            new RandomHelper();
            new TeamHelper();
            new PortalNodeHelper();
            SetBattleData(battleID);
            LoadPath();
        }

        private void SetBattleData(string battleID)
        {
            _battle = SDBattle.GetElement(battleID);
            _battleType = ConvertHelper.ConvertToEnum<BattleType>(_battle.BattleType);
        }

        private void LoadPath()
        {
            string path = AstarUtil.PATHDATA_PATH + _battle.ScenePath;
            TextAsset file = WGLoader.LoadRes<TextAsset>(path, ".txt");
            string[] lines = file.text.Split('\n');
            AstarUtil.LoadMap(lines);
            SubStrateMgr.LoadSubStrateModel();
        }

        private void Clear()
        {
            //TODO

        }
    }
}
