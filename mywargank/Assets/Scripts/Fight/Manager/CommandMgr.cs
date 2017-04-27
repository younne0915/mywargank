using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util;

namespace WG
{
    public class CommandMgr :Singleton<CommandMgr>
    {
        private int _nextFrameIndex = 0;

        private static readonly object sys = new object();

        private List<PlayerCommand> _waitExecuteQueue = new List<PlayerCommand>();

        public void PushCommand(Command cmd)
        {
            PlayerCommand playerCmd = GenerateCommand(cmd);
            if(playerCmd == null)
            {
                return;
            }
            WGLogger.Log(LogModule.Debug, "1");
            playerCmd.SetExecutedFrame(cmd.executeFrame);
            WGLogger.Log(LogModule.Debug, "2");
            PushCommandIntoWaitQueue(playerCmd);
            WGLogger.Log(LogModule.Debug, "3");
        }

        public PlayerCommand GenerateCommand(Command command)
        {
            PlayerCommand playerCmd = null;
            PlayerCommandType cmdType = ConvertHelper.ConvertToEnum<PlayerCommandType>(command.type);
            switch (cmdType)
            {
                case PlayerCommandType.CreateBuilding:
                    playerCmd = new CreateBuildingCmd(command);
                    break;
            }

            return playerCmd;
        }

        public void PushCommandIntoWaitQueue(PlayerCommand playerCmd)
        {
            lock (sys)
            {
                int index = -1;
                for (int i = _waitExecuteQueue.Count - 1; i >= 0; i--)
                {
                    if(playerCmd.executedFrame > _waitExecuteQueue[i].executedFrame)
                    {
                        WGLogger.Log(LogModule.Debug, "4");
                        index = i;
                        break;
                    }else if(IsSameCommand(playerCmd, _waitExecuteQueue[i]))
                    {
                        WGLogger.Log(LogModule.Debug, "5");
                        return;
                    }
                }
                ++index;
                _waitExecuteQueue.Insert(index, playerCmd);
                WGLogger.Log(LogModule.Debug, "6");
            }
        } 

        private bool IsSameCommand(PlayerCommand a, PlayerCommand b)
        {
            if (a.frameId == b.frameId && a.executedFrame == b.executedFrame && a.numberID == b.numberID && a.CommandType == b.CommandType)
            {
                return true;
            }
            return false;
        }

        public void DoCommandAtFrame(int frame)
        {
            lock (sys)
            {
                for (; _nextFrameIndex < _waitExecuteQueue.Count; _nextFrameIndex++)
                {
                    if(_waitExecuteQueue[_nextFrameIndex].executedFrame == frame)
                    {
                        _waitExecuteQueue[_nextFrameIndex].Execute();
                    }
                }
            }
        }
    }
}
