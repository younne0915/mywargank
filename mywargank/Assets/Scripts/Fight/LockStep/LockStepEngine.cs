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
