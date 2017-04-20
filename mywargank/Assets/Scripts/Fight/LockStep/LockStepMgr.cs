using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WG;
using Util;

namespace LockStep
{
    public class LockStepMgr : Singleton<LockStepMgr>
    {
        private int _clientStartDelayFrame = 20;
        public int clientStartDelayFrame
        {
            get { return _clientStartDelayFrame; }
        }

        public void SetClientStartDelayFrame(int frame)
        {
            _clientStartDelayFrame = frame;
        }

        private int _nextKeyFrame = 0;
        public int nextKeyFrame
        {
            get { return _nextKeyFrame; }
        }
        public void SetNextKeyFrame(int nextKeyFrame)
        {
            _nextKeyFrame = nextKeyFrame;
        }

        private bool _startLockStep = false;
        public bool startLockStep
        {
            get { return _startLockStep; }
        }

        private int _keyFrameInterval = 0;
        public int keyFrameInterval
        {
            get { return _keyFrameInterval; }
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

        public void BeganLockStep()
        {
            LockStepEngine.Init();
            _startLockStep = true;
            LockStepEngine.Began();
        }

        public long GetLockStepStartTime(int nextKeyFrame)
        {
            int ms = LockStepEngine.frameInterval * (nextKeyFrame - _clientStartDelayFrame - _keyFrameInterval);
            return ConvertHelper.ConvertDataTimeLong(DateTime.Now) - ms;
        }
    }
}
