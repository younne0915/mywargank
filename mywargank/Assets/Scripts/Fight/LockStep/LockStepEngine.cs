using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LockStep
{
    public class LockStepEngine
    {
        private static bool _beginLockStep = false;

        private static int _frameEachSecond = 20;
        public static int frameEachSecond
        {
            get { return _frameEachSecond; }
        }

        private static int _frameInterval = 50;
        public static int frameInterval
        {
            get { return _frameInterval; }
        }

        private static int _nextKeyLogicFrame = 0;
        public static int nextKeyLogicFrame
        {
            get { return _nextKeyLogicFrame; }
        }

        public static void SetNextKeyLogicFrame(int nextKeyLogicFrame)
        {
            _nextKeyLogicFrame = nextKeyLogicFrame;
        }

        private static int _curLogicFrame = 0;
        private static int _maxLogicFrame = 0;

        private static long _lastRenderFrameTimePoint = -1;
        private static long _deltTime = -1;

        private static long _logicFrameChronoscope = 0;

        public static void Init()
        {
            _frameInterval = 1000 / _frameEachSecond;
        }

        public static void Began()
        {
            if (_beginLockStep) return;
            _beginLockStep = true;

        }

        public static void Update(long time)
        {
            if (!_beginLockStep) return;

            _deltTime = time - _lastRenderFrameTimePoint;
            _lastRenderFrameTimePoint = time;

            _logicFrameChronoscope += _deltTime;

            while(_logicFrameChronoscope > _frameInterval)
            {
                _logicFrameChronoscope -= _frameInterval;

                if (_curLogicFrame < _maxLogicFrame)
                {
                    _curLogicFrame++;
                    //TODO 逻辑帧处理
                }
                else
                {
                    //TODO
                }
                
            }
        }
    }
}
