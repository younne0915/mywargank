﻿using System;
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
        private long _timeSinceLockStart = 0;

        private long _lockStepStartTime = 0;
        public void SetLockStepStartTime(long beganTime)
        {
            WGLogger.LogError(LogModule.Debug, "SetLockStepStartTime"+ _lockStepStartTime);
            _lockStepStartTime = beganTime;
            _startLock = true;
        }

        public void ResetLockStepStartTime(long beganTime)
        {
            if(_lockStepStartTime - beganTime > 50)
            {
                WGLogger.LogError(LogModule.Debug, "ResetLockStepStartTime");
                _lockStepStartTime = beganTime;
            }
        }

        public void Update()
        {
            if (_startLock)
            {
                _timeSinceLockStart = ConvertHelper.ConvertDataTimeLong(DateTime.Now) - _lockStepStartTime;
            }
        }
    }
}
