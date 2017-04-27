using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util;

namespace WG
{
    public enum PlayerCommandType
    {
        CreateBuilding = 0,
        UpgradeBuilding,
        SoldBuilding,
        RefreshCard,
        Rush,
        Wait,
        ReleaseSkill,
        UpgradeCrystalCmd,
        ChatCmd,
    }

    public class PlayerCommand
    {
        public int frameId;
        public int numberID;
        public int executedFrame;

        protected PlayerCommandType commandType;
        public PlayerCommandType CommandType
        {
            get { return commandType; }
        }

        public PlayerCommand(Command cmd)
        {
            commandType = ConvertHelper.ConvertToEnum<PlayerCommandType>(cmd.type);
            numberID = cmd.numberID;
        }

        public void SetExecutedFrame(int frame)
        {
            executedFrame = frame;
        }

        public virtual void Execute()
        {
            WGLogger.Log(LogModule.Pomelo, string.Format("execute {0} at time {1} at frame {2} ", commandType.ToString(), System.DateTime.Now.TimeOfDay.ToString(), LockStep.LockStepEngine.curLogicFrame));

        }
    }
}
