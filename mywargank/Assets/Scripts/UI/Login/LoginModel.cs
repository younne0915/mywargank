using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public class LoginData
    {
        public int numberID;
        public string nickname;
        public int integral;
        public int grade;
        public int portrait;
        public int gold;
        public int jewel;
        public int isGaming;
        public int nextGuideStep;
        public int nextTrainingStep;
        public string serverTime;
        public int isTowering;
    }

    public class LoginModel
    {
        private LoginController _controller;

        public LoginModel(LoginController controller)
        {
            _controller = controller;
        }
    }
}
