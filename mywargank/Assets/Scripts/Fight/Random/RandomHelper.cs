using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public class RandomHelper
    {
        public static RandomHelper instance = null;
        private RandomGenerator _randomGenerator = null;

        public RandomHelper()
        {
            instance = this;
        }

        public void InitWithSeed(int seed)
        {
            //TODO
            //_randomGenerator = new RandomGenerator();
        }
    }
}
