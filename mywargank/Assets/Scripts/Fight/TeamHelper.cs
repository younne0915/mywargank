using Util;
using System.Collections.Generic;

namespace WG
{
    public class TeamHelper : Singleton<TeamHelper>
    {
        private List<Team> _teamList = new List<Team>();

        public override void Clear(bool clearInstance)
        {
            _teamList.Clear();
            base.Clear(clearInstance);
        }

        public List<Team> teamList
        {
            get { return _teamList; }
        }

        public Team GetOrCreatTeam(TeamNum num)
        {
            for (int i = 0; i < _teamList.Count; i++)
            {
                if (_teamList[i].teamNum == num)
                    return _teamList[i];
            }
            Team team = new Team(num);
            _teamList.Add(team);
            return team;
        }

        public bool IsEnemyTeam(Team a, Team b)
        {
            return a.teamNum != b.teamNum;
        }

        public Team GetTeam(TeamNum num)
        {
            for (int i = 0; i < _teamList.Count; i++)
            {
                if (_teamList[i].teamNum == num)
                    return _teamList[i];
            }
            return null;
        }

        public Team GetCompetitorTeam(Team team)
        {
            if (team.teamNum == TeamNum.Team_Left)
                return GetTeam(TeamNum.Team_Right);
            else if (team.teamNum == TeamNum.Team_Right)
                return GetTeam(TeamNum.Team_Left);
            return null;
        }

        public List<Team> GetEnemyTeams(Team team)
        {
            List<Team> enemyTeam = new List<Team>();
            for (int i = 0; i < _teamList.Count; i++)
            {
                if (_teamList[i] != team)
                {
                    enemyTeam.Add(_teamList[i]);
                }
            }
            return enemyTeam;
        }

        public List<Team> GetAllianceTeams(Team team)
        {
            return new List<Team>() { team };
        }

    }
}