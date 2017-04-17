using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util;

namespace WG
{
    public class PortalNodeHelper
    {
        public static PortalNodeHelper instance = null;

        private List<PortralNode> _portralNodeList = new List<PortralNode>();

        public PortalNodeHelper()
        {
            instance = this;
        }

        public void InitPortral(TeamNum teamNum, AStarNode node)
        {
            for (int i = 0; i < _portralNodeList.Count; i++)
            {
                if (_portralNodeList[i].teamNum == teamNum)
                {
                    WGLogger.LogError(LogModule.Debug, "Already add portralNode with team " + teamNum);
                    return;
                }
                _portralNodeList.Add(new PortralNode(teamNum, node));
            }
        }
    }
}
