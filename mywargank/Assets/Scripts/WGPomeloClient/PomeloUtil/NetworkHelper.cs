using Simon.CustomSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public enum PomeloCode
    {
        Fail,
        Success,

    }

    public class NetworkHelper
    {
        public static bool CheckSocketIsConnected(SimonSocketResult result)
        {
            return result.result == System.Net.Sockets.SocketError.Success;
        }

        public static bool CheckPomeloCodeIsSuccess(int code)
        {
            return (int)PomeloCode.Success == code;
        }

        public static bool CheckPomeloResultIsSuccess(PomeloResult result)
        {
            return (int)PomeloCode.Success == result.code;
        }
    }
}
