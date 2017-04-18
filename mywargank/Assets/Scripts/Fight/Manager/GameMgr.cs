using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WG
{
    public class GameMgr : MonoBehaviour
    {
        protected static GameMgr _instance = null;

        void Awake()
        {
            _instance = this;
            CreateBattleUI();
        }

        public static GameMgr getInstance()
        {
            return _instance;
        }

        public void CreateBattleUI()
        {
            if(BattleUIMgr.getInstance() == null)
            {
                UIManager.CreateUI(Constant.UI_BATTLE_PATH + "BattleUI");
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
