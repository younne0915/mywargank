using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public enum TeamNum
    {
        None = 0,
        Team_Left,
        Team_Right,
        Team_NPC,
    }

    public enum PlayerSeat
    {
        None = 0,
        Left_1,
        Right_1,
        Left_2,
        Right_2,
        Left_3,
        Right_3,
    }

    public class Team
    {
        private TeamNum _teamNum;
        public TeamNum teamNum
        {
            get { return _teamNum; }
        }

        public Team(TeamNum teamNum)
        {
            _teamNum = teamNum;
        }

        private int _hp;
        public int hp
        {
            get { return _hp; }
        }

        private int _hpMax;

        public void RemoveHP(int damage)
        {
#if AITest
            damage = 0;
#endif
            _hp -= damage;
            UpdateHP();
            if (_hp <= 0)
            {
                if (PlayerMgr.getInstance().ownTeam == this)
                {
                    FightManager.getInstance().GameOver(BattleResult.Lose);
                }
                else
                {
                    FightManager.getInstance().GameOver(BattleResult.Win);
                }
            }
        }

        public void InitHP(int hp)
        {
            _hp = hp;
            _hpMax = hp;
            UpdateHP();
        }

        public Action<int, int> onLifeChanged;
        public void UpdateHP()
        {
            if (onLifeChanged != null)
            {
                onLifeChanged(_hp, _hpMax);
            }
        }

    }
}
