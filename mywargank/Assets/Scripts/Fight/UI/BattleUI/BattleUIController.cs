using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WG
{
    public class BattleUIController : Singleton<BattleUIController>
    {
        private BattleUI _battleUI;

        private VersusUI _versusUI;

        public void OpenBattleUI()
        {
            if(_battleUI == null)
            {
                GameObject go = UIManager.CreateUI(Constant.UI_BATTLE_PATH + BattleUI.BATTLEUI_PREFAB_NAME);
                _battleUI = go.GetComponent<BattleUI>();
            }
            else
            {
                _battleUI.ShowUI();
            }
        }

        public void OpenVersusUI()
        {
            if (_versusUI == null)
            {
                GameObject go = UIManager.CreateUI(Constant.UI_BATTLE_PATH + VersusUI.VERSUSUI_PREFAB_NAME);
                _versusUI = go.GetComponent<VersusUI>();
            }
            else
            {
                _versusUI.ShowUI();
            }
            InitVersusData();
        }

        private void InitVersusData()
        {
            //TODO 显示左右玩家数据
        }

        public void DestroyVersusUI()
        {
            if (_versusUI != null)
            {
                _versusUI.ClearUI();
            }
        }

        public override void Clear(bool clearInstance)
        {

            if(_battleUI != null)
            {
                _battleUI.ClearUI();
            }

            if(_versusUI != null)
            {
                _versusUI.ClearUI();
            }

            base.Clear(clearInstance);
        }
    }
}
