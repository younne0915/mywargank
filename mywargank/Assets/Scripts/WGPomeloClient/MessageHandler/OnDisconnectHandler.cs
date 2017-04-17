using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public class OnDisconnectHandler : IMsgHandler
    {
        private PomeloClientType _type;
        private string _reason;

        public OnDisconnectHandler(PomeloClientType type, object json)
        {
            _type = type;
            _reason = json.ToString();
        }

        public void ExcuteMsg()
        {
            PomeloStatusMgr.DisconnectServerByPomelo(_type, _reason);
        }
    }
}
