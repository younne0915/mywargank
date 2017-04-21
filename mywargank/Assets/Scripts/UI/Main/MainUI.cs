using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace WG
{
    public class MainUI : BaseUIWindow
    {
        public const string MAINUI_PREFAB_NAME = "MainUIPanel";

        public UIButton matchBtn;

        private MainUIController _controller;

        public void Init(MainUIController controller)
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
                StateMachineController.getInstance().SetNextState(GameStateType.Match);
            }
        }

        void Update()
        {
            WGLogger.LogError(LogModule.Debug,"renderFrameInterval = "+Time.deltaTime);
        }
    }
}
