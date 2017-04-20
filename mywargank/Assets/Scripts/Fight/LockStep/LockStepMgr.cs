using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WG;

namespace LockStep
{
    public class LockStepMgr : Singleton<LockStepMgr>
    {
        private int _keyFrameInterval = 0;
        public int keyFrameInterval
        {
            get { return _keyFrameInterval; }
        }

        private int _nextKeyFrame = 0;
        public int nextKeyFrame
        {
            get { return _nextKeyFrame; }
        }

        private bool _startLockStep = false;
        public bool startLockStep
        {
            get { return _startLockStep; }
        }

        public void SetKeyFrameInterVal(int keyFrameInterVal)
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

        public void SetNextKeyFrame(int nextKeyFrame)
        {
            _nextKeyFrame = nextKeyFrame;
        }

        public void BeganLockStep()
        {
            LockStepEngine.Began();
        }
    }
}
