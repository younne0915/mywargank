using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WG
{
    public class ServerTimeMgr : Singleton<ServerTimeMgr>
    {
        private float _lastFrameRealTime = -1;
        private bool _startReckon = false;

        private long _serverTime = 0;
        public long serverTime
        {
            get { return _serverTime; }
            set
            {
                _serverTime = value;
                _startReckon = true;
            }
        }

        public void Update()
        {
            if (_startReckon)
            {
                if (_lastFrameRealTime < 0)
                {
                    UpdateServerTime(Time.deltaTime);
                }
                else
                {
                    UpdateServerTime(Time.realtimeSinceStartup - _lastFrameRealTime);
                }
                _lastFrameRealTime = Time.realtimeSinceStartup;
            }
        }

        private void UpdateServerTime(float delt)
        {
            _serverTime += (int)(delt * 1000);
        }
    }
}
