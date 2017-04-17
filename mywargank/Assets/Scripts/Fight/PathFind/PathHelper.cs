using PathFind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util;

namespace WG
{
    public class PathHelper
    {
        public static void SetPortalNode(TeamNum teamNum, AStarNode node)
        {
            //TODO
            PortalNodeHelper.instance.InitPortral(teamNum, node);
        }

        public static FixedVector3 GetAstarNodePos(AStarNode node)
        {
            FixedVector3 nodePos = new FixedVector3(node.col* AstarUtil.Grid_Diameter, FixedNum.zero, -node.row*AstarUtil.Grid_Diameter);
            return nodePos;
        }
    }
}
