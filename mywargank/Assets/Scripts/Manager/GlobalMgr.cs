using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WG
{
    public class GlobalMgr : MonoBehaviour
    {
        public static GlobalMgr instance = null;
        void Awake()
        {
            instance = this;
        }
        
        void Start()
        {
            StaticDataLoader.LoadAllJson();
            new IPMgr();
            new StateMachineController();
            GameEntrance.Entrance();
            new StaticDataMgr();
            new PeaceServerTimeMgr();
        }

        void Update()
        {
            if(StateMachineController.instance != null)
            {
                StateMachineController.instance.Update();
            }
            if(PeaceServerTimeMgr.instance != null)
            {
                PeaceServerTimeMgr.instance.Update();
            }
        }
    }
}
