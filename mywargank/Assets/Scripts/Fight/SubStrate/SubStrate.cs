using PathFind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public enum SubStrateType
    {
        Hero,
        Crystal,
    }

    public class SubStrate
    {
        public PlayerSeat seat;
        public int row;
        public int col;
        public TeamNum teamNum;

        public int id;

        private SubStrateType _subStrateType;

        private bool _isDefaultCrystal = false;
        public bool isDefaultCrystal
        {
            get { return _isDefaultCrystal; }
        }

        private AStarNode _createCharacterNode = null;
        public AStarNode createCharacterNode
        {
            get { return _createCharacterNode; }
        }

        public SubStrateComponent view;

        public void SetView(SubStrateComponent component)
        {
            view = component;
            view.SetSusbStrateData(this);
        }

        public SubStrate(PlayerSeat seat, AStarNode node, SubStrateType type, TeamNum teamNum)
        {
            this.seat = seat;
            this.row = node.row;
            this.col = node.col;
            _subStrateType = type;
            this.teamNum = teamNum;
            this.id = row * 1000 + col;
            SetCreateCharacterNode();
            SetAstarNode(node);
            SubStrateMgr.allSubStrate.Add(id, this);

        }

        private AStarNode _astarNode;
        public AStarNode astarNode
        {
            get { return _astarNode; }
        }

        private void SetAstarNode(AStarNode node)
        {
            _astarNode = node;
        }

        private void SetCreateCharacterNode()
        {
            _createCharacterNode = AstarUtil.GetAStarNode(row, col);
        }

        public void SetIsDefaultCrystal()
        {
            _isDefaultCrystal = true;
        }
    }
}
