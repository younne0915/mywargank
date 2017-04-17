using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WG
{
    public class BattleUIMgr : MonoBehaviour
    {
        public static BattleUIMgr instance = null;

        public UIButton lookLeftBtn;
        public UIButton lookRightBtn;

        void Awake()
        {
            instance = this;
            RegisterBTN();
        }

        void RegisterBTN()
        {
            EventDelegate.Add(lookLeftBtn.onClick, OnBtnClick);
            EventDelegate.Add(lookLeftBtn.onClick, OnBtnClick);
        }

        void OnBtnClick()
        {
            if(UIButton.current == lookLeftBtn)
            {
                OnBackHome();
            }
            else if(UIButton.current == lookRightBtn)
            {
                OnBackFront();
            }
            
        }

        public void OnBackFront()
        {

        }

        public void OnBackHome()
        {

        }
    }
}
