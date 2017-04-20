using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public class Singleton<T> where T: class, new()
    {
        protected static T _instance;
        private static readonly object sys = new object();

        public static T getInstance()
        {
            if(_instance == null)
            {
                lock(sys)
                {
                    _instance = new T();
                }
            }
            return _instance;
        }

        public virtual void Clear(bool clearInstance)
        {
            if (clearInstance) _instance = null;
        }
    }
}
