using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WG
{
    public class PomeloMsgRecv : MonoBehaviour
    {
        public static PomeloMsgRecv instance = null;

        private List<IMsgHandler> _msgList = new List<IMsgHandler>();
        private static object obj = new object();

        void Awake()
        {
            instance = this;
        }

        public void RecevMsg(IMsgHandler msg)
        {
            lock(obj)
            {
                _msgList.Add(msg);
            }
        }

        void Update()
        {
            //lock (obj)
            //{
            //    for (int i = 0; i < _msgList.Count; i++)
            //    {
            //        _msgList[i].ExcuteMsg();
            //    }
            //    _msgList.Clear();
            //}
            lock (obj)
            {
                for (int i = 0; i < _msgList.Count;)
                {
                    _msgList[0].ExcuteMsg();
                    _msgList.RemoveAt(0);
                }
            }
        }
    }
}
