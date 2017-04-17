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


    }
}
