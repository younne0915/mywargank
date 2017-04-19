using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WG
{
    public class BaseUIWindow : MonoBehaviour
    {
        public virtual void ShowUI()
        {
            gameObject.SetActive(true);
        }

        public virtual void HideUI()
        {
            gameObject.SetActive(false);
        }

        public virtual void DestroyUI()
        {
            Destroy(gameObject);
        }

        public virtual void RefresUI()
        {

        }

        public virtual void ClearUI()
        {
            DestroyUI();
        }
    }
}
