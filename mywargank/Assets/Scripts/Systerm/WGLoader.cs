using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WG
{
    public class WGLoader
    {
        public static T LoadRes<T>(string path,string variant) where T : Object
        {
#if ABLoad

#else
            string prePath = string.Empty;
            if (string.Equals(variant, ".shader") || string.Equals(variant, ".ttf"))
            {
                prePath = Constant.ABRESOURCE_PATH;
            }
            else
            {
                prePath = Constant.RESOURCESDATA_PATH;
            }
            T obj = AssetDatabase.LoadAssetAtPath<T>(prePath+path + variant) as T;
            return obj;
#endif
        }

        public static GameObject InstantiatePrefab(string path)
        {
            string totalPath = Constant.RESOURCESDATA_PATH + path;
            GameObject go = LoadRes<GameObject>(path, ".prefab");
            go = GameObject.Instantiate<GameObject>(go);
            return go;
        }
    }
}
