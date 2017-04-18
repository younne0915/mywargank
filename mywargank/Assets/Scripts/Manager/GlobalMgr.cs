using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WG
{
    public class GlobalMgr : MonoBehaviour
    {
        protected static GlobalMgr _instance = null;
        void Awake()
        {
            _instance = this;
        }

        public static GlobalMgr getInstance()
        {
            return _instance;
        }

        void Start()
        {
            StaticDataLoader.LoadAllJson();
            IPMgr.getInstance().Init();
            GameEntrance.Entrance();
            new PeaceServerTimeMgr();
        }

        void Update()
        {
            StateMachineController.getInstance().Update();
            PeaceServerTimeMgr.getInstance().Update();
        }
    }
}
