using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WG
{
    public class BattleUIMgr : MonoBehaviour
    {
        protected static BattleUIMgr _instance = null;

        public UIButton lookLeftBtn;
        public UIButton lookRightBtn;

        void Awake()
        {
            _instance = this;
            RegisterBTN();
        }

        public static BattleUIMgr getInstance()
        {
            return _instance;
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
