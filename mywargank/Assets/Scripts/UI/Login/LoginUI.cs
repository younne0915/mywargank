using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WG
{
    public class LoginUI : BaseUIWindow
    {
        public static readonly string LOGINUI_PREFAB_NAME = "LoginPanel";

        public UIButton loginBtn;

        protected LoginController _controler;

        void Awake()
        {
            RegisterBTN();
        }

        void RegisterBTN()
        {
            EventDelegate.Add(loginBtn.onClick, OnBtnClick);
        }

        public void Init(LoginController controller)
        {
            _controler = controller;
        }

        void OnBtnClick()
        {
            if(UIButton.current == loginBtn)
            {
                _controler.SendLogin();
            }
        }

        public override void ClearUI()
        {
            DestroyUI();
        }
    }
}
