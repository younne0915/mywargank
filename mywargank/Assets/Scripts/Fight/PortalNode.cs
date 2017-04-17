using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util;

namespace WG
{
    public class PortralNode
    {
        private TeamNum _teamNum;
        public TeamNum teamNum
        {
            get { return _teamNum; }
        }

        private AStarNode _node;
        public AStarNode node
        {
            get { return _node; }
        }

        public PortralNode(TeamNum teamNum, AStarNode node)
        {
            _teamNum = teamNum;
            _node = node;
        }
    }
}
