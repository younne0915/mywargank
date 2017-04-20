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

        public static void Init()
        {
            _frameInterval = 1000 / _frameEachSecond;
        }

        public static void Began()
        {
            if (_beginLockStep) return;
            _beginLockStep = true;

        }

        public static void Update()
        {

        }
    }
}
