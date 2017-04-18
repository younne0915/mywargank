using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WG;

namespace LockStep
{
    public class LockStepHelper : Singleton<LockStepHelper>
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

        public int frameEachSecond
        {
            get { return LockStepEngine.frameEachSecond; }
        }

        public int ConvertTimeToFrame(int ms)
        {
            return frameEachSecond * ms / 1000;
        }
    }
}
