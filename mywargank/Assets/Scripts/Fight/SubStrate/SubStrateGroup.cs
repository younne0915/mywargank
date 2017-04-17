using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WG
{
    public class SubStrateGroup : MonoBehaviour
    {
        void Awake()
        {
            SubStrateMgr.subStateGroupList.Add(this);
        }

        static void Clear()
        {
            SubStrateMgr.subStateGroupList.Clear();
        }

        void OnDestroy()
        {
            Clear();
        }
    }
}
