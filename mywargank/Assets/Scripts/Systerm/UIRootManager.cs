using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WG
{
    public class UIRootManager : MonoBehaviour
    {
        public static UIRootManager instance = null;
        void Awake()
        {
            instance = this;
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
