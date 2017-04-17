using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public class OnConnectGateBackHandler : IMsgHandler
    {
        private PomeloClientType _type;
        public OnConnectGateBackHandler(PomeloClientType type)
        {
            _type = type;
        }

        public void ExcuteMsg()
        {
            PomeloStatusMgr.GetParticPomeloClient(_type).GetConnectorIPFromGate();
        }
    }
}
