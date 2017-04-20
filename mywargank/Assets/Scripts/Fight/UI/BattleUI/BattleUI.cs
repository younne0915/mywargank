﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WG
{
    public class BattleUI : BaseUIWindow
    {
        public static readonly string BATTLEUI_PREFAB_NAME = "BattleUI";

        public BattleBuildUI battleBuildUI;
        public BattleUpgradeUI bottomUpgradeUI;
        public BattleCardInfo battleCardInfoUI;
        public BattleProgress battleProgress;
        public BattleCharacterUI bottomCharacterUI;
        public BattleSkillInfo battleSkillInfo;
        public BattleSkillInfoUI bottomSkillInfoUI;
        public SkillCardInfo skillCardInfo;
        public BattleHomeBaseUI bottomHomeBaseUI;

        public UIButton lookLeftBtn;
        public UIButton lookRightBtn;

        public UIButton testbTN;

        void Awake()
        {
            RegisterBTN();
        }

        void RegisterBTN()
        {
            EventDelegate.Add(lookLeftBtn.onClick, OnBtnClick);
            EventDelegate.Add(lookLeftBtn.onClick, OnBtnClick);
            EventDelegate.Add(testbTN.onClick, OnBtnClick);
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
            else if(UIButton.current == testbTN)
            {

            }
            
        }

        private void SendTestCmd()
        {

        }

        public void OnBackFront()
        {

        }

        public void OnBackHome()
        {

        }
    }
}
