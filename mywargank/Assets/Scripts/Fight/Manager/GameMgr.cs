using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WG
{
    public class GameMgr : MonoBehaviour
    {
        public static GameMgr instance = null;

        void Awake()
        {
            instance = null;
            CreateBattleUI();
        }

        public void CreateBattleUI()
        {
            if(BattleUIMgr.instance == null)
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
