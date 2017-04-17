using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public class PlayerMgr
    {
        public static PlayerMgr instance = null;

        private Player _ownPlayer;
        public Player ownPlayer
        {
            get { return _ownPlayer; }
        }

        private Player _npcPlayer;
        public Player npcPlayer
        {
            get{ return _npcPlayer; }
        }

        public PlayerMgr()
        {
            instance = this;
        }

        
    }
}
