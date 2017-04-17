using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WG
{
    public class Constant
    {
        #region UI
        public static readonly string ABRESOURCE_PATH = "Assets/ABResource/";
        public static readonly string RESOURCESDATA_PATH = "Assets/ABResource/ResourcesData/";
        public static readonly string UI_LOGIN_PATH = "Prefab/UI/LoginUI/";
        public static readonly string UI_MAIN_PATH = "Prefab/UI/MainUI/";
        public static readonly string UI_MATCH_PATH = "Prefab/UI/MatchUI/";
        public static readonly string UI_BATTLE_PATH = "Prefab/UI/BattleUI/";

        #endregion

        #region Pomelo
        public static string REGION {
            get
            {
                return "China";
            }
        }

        public static string PLATFORM
        {
            get
            {
                var platform = string.Empty;
                switch (Application.platform)
                {
                    case RuntimePlatform.WindowsEditor:
                        {
                            platform = "Windows";
                            break;
                        }
                    case RuntimePlatform.Android:
                        {
                            platform = "Android";
                            break;
                        }
                    case RuntimePlatform.IPhonePlayer:
                        {
                            platform = "IOS";
                            break;
                        }
                }
                return platform;
            }
        }

        public static readonly string NUMBERID = "numberID";

        public static readonly string GATE_FILE_NAME = "Gate";

        #endregion

        public static string APP_VERSION = "1.0"; 
    }
}