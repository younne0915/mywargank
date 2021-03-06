﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WG
{
    public class MainUIController : Singleton<MainUIController>
    {

        private MainUI _mainUI;

        public MainUI mainUI
        {
            get{ return _mainUI;}
        }

        public MainUIController()
        {
            
        }

        public void OpenMainUI()
        {
            if(_mainUI == null)
            {
                GameObject go = UIManager.CreateUI(Constant.UI_MAIN_PATH + MainUI.MAINUI_PREFAB_NAME);
                _mainUI = go.GetComponent<MainUI>();
                _mainUI.Init(this);
            }
            else
            {
                _mainUI.ShowUI();
            }
            _mainUI.RefresUI();
        }

        public override void Clear(bool clearInstance)
        {
            MatchUIController.getInstance().Clear(clearInstance);
            if(_mainUI != null)
            {
                _mainUI.ClearUI();
            }
            base.Clear(clearInstance);
        }
    }
}
