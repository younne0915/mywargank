﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LockStep;

namespace WG
{
    public class TimeMgr:Singleton<TimeMgr>
    {
        private LockStepTime _lockStepTime;

        public long timeSinceLockStart
        {
            get { return _lockStepTime.timeSinceLockStart; }
        }

        public TimeMgr()
        {
            _lockStepTime = new LockStepTime();
        }

        public void SetLockStepStartTime(long beganTime)
        {
            _lockStepTime.SetLockStepStartTime(beganTime);
        }

        public void ResetLockStepStartTime(long beganTime)
        {
            _lockStepTime.ResetLockStepStartTime(beganTime);
        }

        public void Update()
        {
            _lockStepTime.Update();
        }
    }
}
