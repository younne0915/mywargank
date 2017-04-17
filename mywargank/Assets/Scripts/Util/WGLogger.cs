
#if client
using UnityEngine;
#endif
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Util
{
	public enum LogModule : int
	{
		Debug,
        Login,
		LockStep,
        PathFind,
        Pomelo,
        Friend,
        Http,
        UI,
        Random,
        Character,
	}

	public static class WGLogger
	{
		private static int _loggerMask = -1;
        private static string _logName = "WGLog";
        private static string _logPath = "";
        
        public static void Init()
        {
            #if client
            _logPath = FileHelper.GetPath(_logName);           
            FileHelper.DeleteFile(_logPath);
#endif
            _loggerMask = 1 << (int)LogModule.Pomelo;
			_loggerMask = -1;
        }

        public static void Clear()
        {

        }

		private static bool _enablWrite = true;
		public static bool enableShow = false;

        private static List<string> _logs = new List<string>();

        public static List<string> logs
        {
            get { return _logs; }
        }

        static void Write(string s)
        {
            if (_enablWrite)
            {
                #if client
                FileHelper.WriteFileAppend(s, _logPath);
#endif
            }
        }

        static void AddLogToScreen(string s)
        {
            if (enableShow)
            {
                _logs.Add(s);
            }
        }

		public static void Log(LogModule module,string s)
		{
			if(CheckCanLog(module))
			{
                s = string.Format("[{0}]:{1}", module.ToString(), s);
#if client
                Debug.Log(s);
#else
				System.Console.WriteLine(s);
#endif
                AddLogToScreen(s);
                Write(s);
            }
		}

		public static void LogWarning(LogModule module,string s)
		{
			if(CheckCanLog(module))
			{
                s = string.Format("[{0}]:{1}", module.ToString(), s);
#if client
                Debug.LogWarning(s);
#endif
				AddLogToScreen(s);
				Write(s);
			}
		}

		public static void LogError(LogModule module,string s)
		{
            if(CheckCanLog(module))
            {
                s = string.Format("[{0}]:{1}", module.ToString(), s);
#if client
                Debug.LogError(s);
#else
				System.Console.WriteLine(s);
#endif
				AddLogToScreen(string.Format("[ff0000]{0}[-]",s));
				Write(s);
           }
		}

		public static bool CheckCanLog(LogModule module)
		{
#if Release
            return false;
#endif
            if ((_loggerMask & 1 << System.Convert.ToInt32(module)) != 0)
            {
                return true;
            }
            return false;

        }
    }
}
