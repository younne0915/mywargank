using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WG
{
    public class BattleController : Singleton<BattleController>
    {
        private BattleUI _battleUI;

        public void CreateBattleUI()
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
    }
}
