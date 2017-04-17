using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WG
{
    public class MainUI : BaseUIWindow
    {
        public const string MAINUI_PREFAB_NAME = "MainUIPanel";

        public UIButton matchBtn;

        private MainController _controller;

        public void Init(MainController controller)
        {
            _controller = controller;
        }

        void Awake()
        {
            RegisterBTN();
        }

        void RegisterBTN()
        {
            EventDelegate.Add(matchBtn.onClick, OnBtnClick);
        }

        void OnBtnClick()
        {
            if(UIButton.current == matchBtn)
            {
                OnMatchBtnClick();
            }
        }

        void OnMatchBtnClick()
        {
            if (PomeloStatusMgr.IsServerConnected())
            {
                StateMachineController.instance.SetNextState(GameStateType.Match);
            }
        }
    }
}
