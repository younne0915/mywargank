using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WG
{
    public class CoroutineHelper : MonoBehaviour
    {
        protected static CoroutineHelper _instance = null;

        void Awake()
        {
            _instance = this;
        }

        public static CoroutineHelper getInstance()
        {
            return _instance;
        }

        public void StartCorotineBehavior(IEnumerator routine)
        {
            StartCoroutine(routine);
        }

        public void StopCorotineBehavior(IEnumerator routine)
        {
            StopCoroutine(routine);
        }
    }
}
