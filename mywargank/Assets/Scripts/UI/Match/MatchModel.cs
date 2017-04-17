using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public class MatchModel
    {
        private int _emtime = 0;
        public int emtime
        {
            get { return _emtime; }
        }

        public void SetEmtime(int emtime)
        {
            _emtime = emtime;
        }
    }
}
