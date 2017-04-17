using PathFind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public class AStarMap
    {
        public AStarNode[,] map;

        private List<AStarNode> _nodeList = new List<AStarNode>();

        private int _row = 72;
        public int row
        {
			get{ return _row;}
		}

        private int _col = 200;
        public int col
        {
            get { return _col; }
        }

        public AStarMap(int row, int col)
        {
            _row = row;
            _col = col;
            map = new AStarNode[_row, _col];
            AstarUtil.Grid_Number_Per_Menter = 1 / AstarUtil.Grid_Diameter;
            AstarUtil.Grid_Radius = AstarUtil.Grid_Diameter / 2;
            for (int i = 0; i < _row; i++)
            {
                for (int j = 0; j < _col; j++)
                {
                    AStarNode node = new AStarNode(i, j);
                    node.walkable = true;
                    _nodeList.Add(node);
                    map[i, j] = node;
                }
            }
        }

        public AStarNode GetAStarNode(int x, int y)
        {
            //TODO
            return map[x, y];
        }
    }
}
