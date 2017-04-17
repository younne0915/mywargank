using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WG
{
    public class SubStrateComponent : MonoBehaviour
    {
        public GameObject selectedEffect;


        private SubStrate _sub;
        public void SetSusbStrateData(SubStrate sub)
        {
            _sub = sub;
        }

        public void OnUnSelected()
        {
            selectedEffect.SetActive(false);
            //if(PlayerMgr.instance.ownPlayer.seat == _sub.seat)
            //{
            //    selectedEffect.SetActive(false);
            //}
        }

        public void OnSelected()
        {
            selectedEffect.SetActive(true);
            //if (PlayerMgr.instance.ownPlayer.seat == _sub.seat)
            //{
            //    selectedEffect.SetActive(true);
            //}
        }
    }
}
