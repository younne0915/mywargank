using System.Collections.Generic;
using LitJson;
using Util;

namespace WG
{

    public class PlayerMgr :Singleton<PlayerMgr>
    {

        private List<Player> _playerList = new List<Player>();
        private Dictionary<Team, List<Player>> _playerTeamDic = new Dictionary<Team, List<Player>>();
        private Player _ownPlayer = null;
        //   private Player _competitorPlayer = null;

        public override void Clear(bool clearInstance)
        {
            _playerList.Clear();
            base.Clear(clearInstance);
        }

        public Player ownPlayer
        {
            get { return _ownPlayer; }
        }

        private Player _npcPlayer;
        public Player npcPlayer
        {
            get
            {
                return _npcPlayer;
            }
        }

        public Team ownTeam
        {
            get { return _ownPlayer.team; }
        }

        public Team competitorTeam
        {
            get { return TeamHelper.getInstance().GetCompetitorTeam(ownTeam); }
        }
        //public Player competitorPlayer
        //{
        //    get { return _competitorPlayer; }
        //}

        public List<Player> playerList
        {
            get { return _playerList; }
        }

        public Player GetPlayerByNumberID(int numberID)
        {
            for (int i = 0; i < _playerList.Count; i++)
            {
                if (_playerList[i].numberID == numberID)
                {
                    return _playerList[i];
                }
            }
            return null;
        }

        public Player GetPlayerBySeat(PlayerSeat seat)
        {
            for (int i = 0; i < _playerList.Count; i++)
            {
                if (_playerList[i].seat == seat)
                {
                    return _playerList[i];
                }
            }
            return null;
        }

        public List<Player> GetPlayerListByTeam(Team team)
        {
            return _playerTeamDic[team];
        }

        public void InitNPCPlayer(User user, int seat, int portrait, TeamNum teamNum)
        {
            _npcPlayer = InitPlayer(user, seat, portrait, false, teamNum);
        }

        public Player InitPlayer(User user, int seat, int portrait, bool isOwnPlayer, TeamNum teamNum)
        {
            Team team = TeamHelper.getInstance().GetOrCreatTeam(teamNum);
            Player player = new Player(user);
            player.team = team;
            _playerList.Add(player);
            if (!_playerTeamDic.ContainsKey(team))
            {
                _playerTeamDic[team] = new List<Player>();
            }
            _playerTeamDic[team].Add(player);

            player.SetSeat((PlayerSeat)seat);
            player.SetPortrait(portrait);
            if (isOwnPlayer)
            {
                _ownPlayer = player;
            }
            return player;
        }

        public void Update(int frame)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                playerList[i].Update(frame);
            }
        }

        public void InitCrystal()
        {
            //foreach (SubStrate ss in SubStrateGroupData.allSubStrate.Values)
            //{
            //    if (ss.isDefaultCrystal)
            //    {
            //        for (int i = 0; i < playerList.Count; i++)
            //        {
            //            if (ss.seat == playerList[i].seat)
            //            {
            //                playerList[i].CreateBuilding(ss.id, Common.FightCommonMgr.instance.sdBattle.CrystalCardId);
            //            }
            //        }
            //    }
            //}

        }

    }
}