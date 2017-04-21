using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util;

namespace WG
{
    public class CommandMgr :Singleton<CommandMgr>
    {
        public void PushCommand(Command cmd)
        {
            PlayerCommand playerCmd = GenerateCommand(cmd);

        }

        public PlayerCommand GenerateCommand(Command command)
        {
            PlayerCommand playerCmd = null;
            PlayerCommandType cmdType = ConvertHelper.ConvertToEnum<PlayerCommandType>(command.type);
            switch (cmdType)
            {
                case PlayerCommandType.CreateBuilding:
                    playerCmd = new CreateBuildingCmd();
                    break;
            }

            return playerCmd;
        }
    }
}
