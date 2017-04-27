using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WG;
using Util;
using UnityEngine;

namespace LockStep
{
    public class LockStepTime
    {

        private long _timeSinceLockStart = 0;
        public long timeSinceLockStart
        {
            get{ return _timeSinceLockStart; }
        }

        private long _lockStepStartTime = 0;
        public void SetLockStepStartTime(long beganTime)
        {
            _lockStepStartTime = beganTime;
        }

        public void ResetLockStepStartTime(long beganTime)
        {
            if(_lockStepStartTime - beganTime > 50)
            {
                _lockStepStartTime = beganTime;
            }
        }

        private float _time = 0;

        public void Update()
        {
            _timeSinceLockStart = ConvertHelper.ConvertDataTimeLong(DateTime.Now) - _lockStepStartTime;
            //WGLogger.LogError(LogModule.Debug, "_timeSinceLockStart = " + _timeSinceLockStart);
        }
    }
}
