using Simon.CustomSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public class OnConnectServerHandler : IMsgHandler
    {
        private PomeloClientType _type;
        private PomeloResult _result;

        public OnConnectServerHandler(PomeloClientType type, PomeloResult result)
        {
            _type = type;
            _result = result;
        }

        public void ExcuteMsg()
        {
            PomeloStatusMgr.GetParticPomeloClient(_type).ExecuteConnectServerCalback(_result);
        }
    }
}
