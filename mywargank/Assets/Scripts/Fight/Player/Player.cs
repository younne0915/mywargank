using LockStep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public class Player
    {
        private string _nickName = "";
        public string nickName
        {
            get { return _nickName; }
        }

        private int _numberID;
        public int numberID
        {
            get { return _numberID; }
        }

        private int _portraitID;
        public int portraitID
        {
            get { return _portraitID; }
        }

        private int _intergral;
        public int intergral
        {
            get { return _intergral; }
        }

        public Team team;

        private PlayerSeat _playerSeat;

        public PlayerSeat seat
        {
            get { return _playerSeat; }
        }

        public void SetPortrait(int portraitID)
        {
            _portraitID = portraitID;
        }

        public void SetIntergral(int intergral)
        {
            _intergral = intergral;
        }

        public void SetSeat(PlayerSeat seat)
        {
            _playerSeat = seat;
            //if (_playerSeat == Faction.Left_1)
            //{
            //    _competitorFaction = Faction.Right_1;
            //}
            //else
            //{
            //    _competitorFaction = Faction.Left_1;
            //}
        }

        private int _rushCD = 0;
        public int rushCD
        {
            get { return _rushCD; }
        }

        private int _refreshCD;
        public int refreshCD
        {
            get { return _refreshCD; }
        }

        private int _lastRushFrame = 0;
        public int lastRushFrame
        {
            get { return _lastRushFrame; }
        }

        private int _lastRefreshFrame = 0;
        public int lastRefreshFrame
        {
            get { return _lastRefreshFrame; }
        }

        public Player(User user)
        {
            _numberID = user.numberID;
            _nickName = user.nickName;
            _rushCD = LockStepHelper.getInstance().ConvertTimeToFrame(FightManager.getInstance().sdBattle.SpeedUpCD);
            _refreshCD = LockStepHelper.getInstance().ConvertTimeToFrame(FightManager.getInstance().sdBattle.ChangeCD);
            _lastRushFrame = -rushCD;
            _lastRefreshFrame = -refreshCD;
        }

        public void Update(int frame)
        {

        }
    }
}
