using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public class RandomHelper : Singleton<RandomHelper>
    {
        private RandomGenerator _randomGenerator = null;

        public void InitWithSeed(int seed)
        {
            //TODO
            //_randomGenerator = new RandomGenerator();
        }
    }
}
