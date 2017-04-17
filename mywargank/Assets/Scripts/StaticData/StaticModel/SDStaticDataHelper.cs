using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public static class SDStaticDataHelper
    {
        public static string GetSDDataIDByLevelAndCid(int level, int cid)
        {
            return (level * 1000 + cid).ToString();
        }
    }
}
