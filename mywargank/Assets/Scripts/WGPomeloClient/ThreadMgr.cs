using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WG
{
    public class ThreadMgr
    {
        public static void RunThread(Action method)
        {
            Thread thread = new Thread(new ThreadStart(method));
            thread.Start();
        }

        public static void RunThread(Action<object> method, object pagma)
        {
            ParameterizedThreadStart pt = new ParameterizedThreadStart(method);
            Thread th = new Thread(pt);
            th.Start(pagma);
        }
    }
}
