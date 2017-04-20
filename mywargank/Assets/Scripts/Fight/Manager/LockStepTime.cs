using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WG;
using Util;

namespace LockStep
{
    public class LockStepTime
    {
        private bool _startLock = false;
        private long _timeSceneLockStart = 0;

        private long _lockStepStartTime = 0;
        public void SetLockStepStartTime(long beganTime)
        {
            _lockStepStartTime = beganTime;
            _startLock = true;
        }

        public void Update()
        {
            if (_startLock)
            {
                _timeSceneLockStart = ConvertHelper.ConvertDataTimeLong(DateTime.Now) - _lockStepStartTime;
            }
        }
    }
}
