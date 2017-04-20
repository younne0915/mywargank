using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LockStep;

namespace WG
{
    public class OnKeyFrameMsgHandler : IMsgHandler
    {
        public static readonly string interfaceName = ServerPushInterface.OnKeyFrame.InterfaceName;
        private ServerPushInterface.OnKeyFrame _result;
        private int nextKeyFrame = 0;

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
            nextKeyFrame = _result.nextKeyFrame;
            
            if (!LockStepMgr.getInstance().startLockStep)
            {
                LockStepMgr.getInstance().BeganLockStep();
            }

            LockStepMgr.getInstance().SetNextKeyFrame(nextKeyFrame);
        }

        public static void SetClientStartDelayFrame(int frame)
        {
            _clientStartDelayFrame = frame;
        }

        
    }
}
