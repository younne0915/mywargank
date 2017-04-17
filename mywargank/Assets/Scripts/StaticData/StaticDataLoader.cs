using Util;
#if client
using UnityEngine;
#endif
using System;
using System.Collections.Generic;
using System.IO;

namespace WG
{
    public static class StaticDataLoader
    {
        static StaticDataLoader()
        {
            JsonHelper.Init();
        }
        public static string GetText(string path)
        {
#if client
            TextAsset file = WGLoader.LoadRes<TextAsset>(path, ".json");
            if (file == null)
            {
                Debug.LogError(string.Format("config file not found: {0}", path));
                return "";
            }        
            string data = file.text;
            //NewAssetBundleLoad.ReleaseLoadedAssetBundle(path);
            Resources.UnloadAsset(file);
            return data;
#else
#if aichecker
            string dataDir = System.IO.Directory.GetCurrentDirectory()+"/";
#elif serverclient
			string dataDir = System.IO.Directory.GetCurrentDirectory() + "/";
#else
            string dataDir = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/");
#endif
            StreamReader sr = new StreamReader(dataDir + path + ".json");
            string data = sr.ReadToEnd();
            return data;
#endif

            return "";
        }

        static readonly string STATIC_DATA_CONFIG_PATH = "StaticData/StaticDataConfig";
        static readonly string STATIC_CONFIG = "SDStaticDataConfig";
        static List<string> classes = new List<string>();
        static void LoadStaticDataConfig()
        {
            classes.Clear();

            Type configType = Type.GetType("WG." + STATIC_CONFIG);
            StaticDataBase obj = Activator.CreateInstance(configType, null) as StaticDataBase;
            obj.LoadData();
            List<SDStaticDataConfig> config = SDStaticDataConfig.GetDataList();
            for (int i = 0; i < config.Count; i++)
            {
                classes.Add(config[i].Id);
            }
            //string config = GetText(STATIC_DATA_CONFIG_PATH);
            //classes = config.Split(',');
        }

        static bool _loadFromServer = true;

        public static void LoadAllJson()
        {
            LoadStaticDataConfig();

            for (int i = 0; i < classes.Count; i++)
            {
                Type configType = Type.GetType("WG." + classes[i]);
                if (configType == null)
                {
                    WGLogger.LogError(LogModule.Debug, "Can not find static data class " + classes[i]);
                }
                StaticDataBase obj = Activator.CreateInstance(configType, null) as StaticDataBase;
                // WGLogger.Log(LogModule.Debug, "Load config " + classes[i]);
                obj.Clear();
                obj.LoadData();
            }
        }

        //public static void LoadSkillJson()
        //{
        //    LoadSkillConfig();
        //    for (int i = 0; i < skillClasses.Length; i++)
        //    {
        //        Type configType = Type.GetType("WG." + classes[i]);
        //        Activator.CreateInstance(configType, null);

        //    }
        //}


    }
}