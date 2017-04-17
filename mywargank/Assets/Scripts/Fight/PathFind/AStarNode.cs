using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public class AStarNode
    {
        public int row;
        public int col;

        public bool walkable = false;
        public int capacity;

        public AStarNode(int x, int y)
        {
            this.row = x;
            this.col = y;
            this.capacity = 10;
        }
    }
}
