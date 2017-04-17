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
        public static CoroutineHelper instance = null;

        void Awake()
        {
            instance = this;
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
