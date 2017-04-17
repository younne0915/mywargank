using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WG
{
    public class MatchUI : BaseUIWindow
    {
        public static readonly string MATCHUI_PREFAB_NAME = "MatchUIPanel";

        public UIButton cancleBtn;
        public UILabel emTimeLabel;

        private MatchController _controller;

        public void Init(MatchController controller)
        {
            _controller = controller;
        }

        void Awake()
        {
            RegisterBTN();
        }

        void RegisterBTN()
        {
            EventDelegate.Add(cancleBtn.onClick, OnBtnClick);
        }

        void OnBtnClick()
        {
            if(UIButton.current == cancleBtn)
            {
                OnCancleMatchClick();
            }
        }

        private void OnCancleMatchClick()
        {
            //HideUI();
            //StateMachineController.instance.SetNextState(GameStateType.Main);
            _controller.SendCancleMatch();
        }
    }
}
