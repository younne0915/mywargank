using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public class OnConnectVersionServerHandler:IMsgHandler
    {
        private PomeloClientType _type;

        public OnConnectVersionServerHandler(PomeloClientType type)
        {
            _type = type;
        }

        public void ExcuteMsg()
        {
            PomeloStatusMgr.GetParticPomeloClient(_type).GetGateIPFromVersion();
        }
    }
}
