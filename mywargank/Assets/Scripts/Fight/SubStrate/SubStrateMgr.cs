using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WG
{
    public class SubStrateMgr : Singleton<SubStrateMgr>
    {
        public static List<SubStrateGroup> subStateGroupList = new List<SubStrateGroup>();
        public static Dictionary<int, SubStrate> allSubStrate = new Dictionary<int, SubStrate>();

        private SubStrateComponent _curSelectedCom = null;

        public static void LoadSubStrateModel()
        {
            GameObject go = new GameObject();
            GameObject subStrateGo = WGLoader.LoadRes<GameObject>("Prefab/Substrate",".prefab");
            foreach (SubStrate substrate in allSubStrate.Values)
            {
                GameObject sub = GameObject.Instantiate(subStrateGo) as GameObject;
                SubStrateComponent component = sub.GetComponent<SubStrateComponent>();
                substrate.SetView(component);
                component.transform.position = PathHelper.GetAstarNodePos(substrate.astarNode).vector3;
                component.transform.SetParent(go.transform);

                if(substrate.seat == PlayerSeat.Left_1 || substrate.seat == PlayerSeat.Left_2 || substrate.seat == PlayerSeat.Left_3)
                {
                    component.transform.forward = Vector3.right;
                }
                else
                {
                    component.transform.forward = Vector3.left;
                }

                sub.name = substrate.seat + sub.name + "_" + substrate.id;
            }
        }

        public void OnSubstrateSelected(SubStrateComponent subStrateComponent)
        {
            if (subStrateComponent == _curSelectedCom) return;
            if(_curSelectedCom != null)
            {
                _curSelectedCom.OnUnSelected();
            }
            _curSelectedCom = subStrateComponent;
            if(_curSelectedCom != null)
            {
                _curSelectedCom.OnSelected();
            }
        }
    }
}
