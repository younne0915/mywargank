using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WG
{
    public class PeaceServerTimeMgr : Singleton<PeaceServerTimeMgr>
    {
        private float _lastFrameRealTime = -1;
        private bool _startReckon = false;
        private long _serverTime = 0;
        public long serverTime
        {
            get
            {
                return _serverTime;
            }
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

        public void UpdateServerTime(float deltaTime)
        {
            _serverTime += (int)(deltaTime*1000);
        }
        
    }
}
