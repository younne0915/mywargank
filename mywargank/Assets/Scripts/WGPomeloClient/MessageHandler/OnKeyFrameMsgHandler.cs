using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public class OnKeyFrameMsgHandler : IMsgHandler
    {
        public static readonly string interfaceName = ServerPushInterface.OnKeyFrame.InterfaceName;
        private ServerPushInterface.OnKeyFrame _result;

        private static int _clientStartDelayFrame = 20;
        public static int clientStartDelayFrame
        {
            get { return _clientStartDelayFrame; }
        }

        public OnKeyFrameMsgHandler(object json)
        {
            _result = (ServerPushInterface.OnKeyFrame)json;
        }

        public void ExcuteMsg()
        {

        }

        public static void SetClientStartDelayFrame(int frame)
        {
            _clientStartDelayFrame = frame;
        }

        
    }
}
