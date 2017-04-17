#if client
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Util
{
    public class WGLoggerHelper : MonoBehaviour
    {
        // Use this for initialization
        void Awake()
        {
            WGLogger.Init();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnApplicationQuit()
        {
            WGLogger.Clear();
        }

#if Debug
        void OnGUI()
        {
            if (WGLogger.enableShow)
            {
                GUIStyle bb = new GUIStyle();
                bb.normal.textColor = new Color(1, 1, 1);
                bb.fontSize = 25;
                List<string> logs = WGLogger.logs;
                int index = logs.Count - 20 > 0 ? logs.Count - 20 : 0;
                for (; index < logs.Count; index++)
                {
                    GUILayout.Label(logs[index]);
                }
            }
        }
#endif

    }
}
#endif