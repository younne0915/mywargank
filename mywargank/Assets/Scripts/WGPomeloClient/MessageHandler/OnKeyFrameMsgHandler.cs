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
                TimeMgr.getInstance().SetLockStepStartTime(LockStepMgr.getInstance().GetLockStepStartTime(nextKeyFrame));
            }
            else
            {
                TimeMgr.getInstance().SetLockStepStartTime(LockStepMgr.getInstance().GetLockStepStartTime(nextKeyFrame));
            }

            LockStepMgr.getInstance().SetNextKeyFrame(nextKeyFrame);
        }

        

        
    }
}
