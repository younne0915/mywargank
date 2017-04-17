using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LockStep
{
    public class LockStepHelper
    {
        private static int _keyFrameInterval = 0;
        public static int keyFrameInterval
        {
            get { return _keyFrameInterval; }
        }

        public static void SetKeyFrameInterVal(int keyFrameInterVal)
        {
            _keyFrameInterval = keyFrameInterval;
        }
    }
}
