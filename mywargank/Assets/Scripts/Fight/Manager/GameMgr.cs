﻿using System.Collections;
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
            OpenVersusUI();
        }

        public static GameMgr getInstance()
        {
            return _instance;
        }

        private void OpenVersusUI()
        {
            CoroutineHelper.getInstance().StartCorotineBehavior(OpenVersusUICorotine());
        }

        private IEnumerator OpenVersusUICorotine()
        {
            BattleUIController.getInstance().OpenVersusUI();
            yield return new WaitForSeconds(int.Parse(SDConstant.GetElement("VS_ANIM_TIME").Value) * 0.001f);
            BattleUIController.getInstance().DestroyVersusUI();
            BattleUIController.getInstance().OpenBattleUI();

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
