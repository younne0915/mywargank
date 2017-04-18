using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WG
{
    public class UIRootManager : MonoBehaviour
    {
        protected static UIRootManager _instance = null;
        void Awake()
        {
            _instance = this;
        }

        public static UIRootManager getInstance()
        {
            return _instance;
        }

        public void AddUI(GameObject ui)
        {
            if(ui != null)
            {
                ui.transform.SetParent(gameObject.transform, false);
            }
        }
        
    }
}
