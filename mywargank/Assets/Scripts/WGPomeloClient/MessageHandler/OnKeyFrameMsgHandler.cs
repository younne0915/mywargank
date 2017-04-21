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
        private int nextKeyLogicFrame = 0;
        private Command command;

        public OnKeyFrameMsgHandler(object json)
        {
            _result = (ServerPushInterface.OnKeyFrame)json;
        }

        public void ExcuteMsg()
        {
            nextKeyLogicFrame = _result.nextKeyFrame;
            
            if (!FightManager.getInstance().startLockStep)
            {
                FightManager.getInstance().BeganLockStepMgr();
                TimeMgr.getInstance().SetLockStepStartTime(LockStepMgr.getInstance().GetLockStepStartTime(nextKeyLogicFrame));
            }
            else
            {
                TimeMgr.getInstance().ResetLockStepStartTime(LockStepMgr.getInstance().GetLockStepStartTime(nextKeyLogicFrame));
            }

            LockStepMgr.getInstance().SetNextKeyLogicFrame(nextKeyLogicFrame);

            if(_result.commands != null)
            {
                for (int i = 0; i < _result.commands.Count; i++)
                {
                    command = new Command(_result.commands[i].executeFrame, _result.commands[i].type, _result.commands[i].paramsList, _result.commands[i].numberID);

                }
            }
            
        }
        
    }
}
