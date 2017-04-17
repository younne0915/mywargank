using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WG
{
    public class StaticDataMgr
    {
        public static StaticDataMgr instance = null;

        public StaticDataMgr()
        {
            instance = this;
        }
    }
}
