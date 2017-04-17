using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WG;
using Util;

namespace PathFind
{
    public class AstarUtil
    {
        public static readonly string PATHDATA_PATH = "PathData/";

        public static FixedNum Grid_Number_Per_Menter;
        public static readonly FixedNum Grid_Diameter = new FixedNum(50, FixedNumRatio.Percentage);
        public static FixedNum Grid_Radius = new FixedNum(25, FixedNumRatio.Percentage);


        private static AStarMap _map;
        public AStarMap map
        {
            get { return _map; }
        }

        public static int mapRow
        {
            get { return _map.row; }
        }

        public static void LoadMap(string[] lines)
        {
            SubStrateMgr.allSubStrate.Clear();

            if(lines.Length > 0)
            {
                string[] col = lines[0].Split(',');
                _map = new AStarMap(lines.Length, col.Length);

                for (int i = 0; i < lines.Length; i++)
                {
                    col = lines[i].Split(',');
                    for (int j = 0; j < col.Length; j++)
                    {
                        if (!string.IsNullOrEmpty(col[j]))
                        {
                            int val = int.Parse(col[j]);
                            LoadSubStrateData(val, _map.GetAStarNode(i, j));
                        }
                    }
                }
            }
        }

        private static void LoadSubStrateData(int val, AStarNode node)
        {
            if(val == 1)//左侧造英雄的小格子
            {
                new SubStrate(PlayerSeat.Left_1, node, SubStrateType.Hero, TeamNum.Team_Left);
            }
            else if(val == 2)
            {
                new SubStrate(PlayerSeat.Right_1, node, SubStrateType.Hero, TeamNum.Team_Right);
            }
            else if(val == 3 || val == 5)
            {
                SubStrate ss = new SubStrate(PlayerSeat.Left_1, node, SubStrateType.Crystal, TeamNum.Team_Left);
                if(val == 5)
                {
                    ss.SetIsDefaultCrystal();
                }
            }
            else if(val == 4 || val == 6)
            {
                SubStrate ss = new SubStrate(PlayerSeat.Right_1, node, SubStrateType.Crystal, TeamNum.Team_Right);
                if(val == 6)
                {
                    ss.SetIsDefaultCrystal();
                }
            }
            else if(val == 9)
            {
                PathHelper.SetPortalNode(TeamNum.Team_Left, node);
            }
            else if(val == 10)
            {
                PathHelper.SetPortalNode(TeamNum.Team_Right, node);
            }
        }

        public static AStarNode GetAStarNode(int row, int col)
        {
            if(row < 0 || row > _map.row || col < 0 || col > _map.col)
            {
                return null;
            }
            return _map.GetAStarNode(row,col);
        }
    }
}
