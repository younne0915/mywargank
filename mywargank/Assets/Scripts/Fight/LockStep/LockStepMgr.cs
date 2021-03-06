﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WG;
using Util;

namespace LockStep
{
    public class LockStepMgr : Singleton<LockStepMgr>
    {
        private int _clientStartDelayLogicFrame = 20;
        public int clientStartDelayLogicFrame
        {
            get { return _clientStartDelayLogicFrame; }
        }

        public void SetClientStartDelayLogicFrame(int logicFrame)
        {
            _clientStartDelayLogicFrame = logicFrame;
        }

        
        public void SetNextKeyLogicFrame(int nextLogicKeyFrame)
        {
            LockStepEngine.SetNextKeyLogicFrame(nextLogicKeyFrame);
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

        public int curLogicFrame
        {
            get { return LockStepEngine.curLogicFrame; }
        }

        public int ConvertTimeToFrame(int ms)
        {
            return frameEachSecond * ms / 1000;
        }

        public void BeganLockStep()
        {
            LockStepEngine.Began();
        }

        public long GetLockStepStartTime(int nextKeyFrame)
        {
            int ms = LockStepEngine.frameInterval * (nextKeyFrame - _clientStartDelayLogicFrame - _keyFrameInterval);
            return ConvertHelper.ConvertDataTimeLong(DateTime.Now) - ms;
        }

        public void Update()
        {
            LockStepEngine.Update(TimeMgr.getInstance().timeSinceLockStart);
        }
    }
}
