using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public class CreateBuildingCmd : PlayerCommand
    {
        public int substraitID;
        public string cardID;

        public CreateBuildingCmd(Command cmd) : base(cmd)
        {
            substraitID = cmd.paramsList[0];
            cardID = cmd.paramsList[1].ToString();
        }
    }
}
