using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WG
{
    public class OnCommonMsgHandler<T> : IMsgHandler
    {
        private object _data;
        private Action<T> _callback;

        public OnCommonMsgHandler(object data, Action<T> callback)
        {
            _data = data;
            _callback = callback;
        }

        public void ExcuteMsg()
        {
            T result = (T)_data;
            if(_callback != null)
            {
                _callback(result);
            }
        }
    }
}
