using UnityEngine;
using System.Collections;

namespace WG
{
    public class UIManager
    {
        public static GameObject CreateUI(string path)
        {
            GameObject ui = WGLoader.InstantiatePrefab(path);
            UIRootManager.instance.AddUI(ui);
            ui.name = ui.name.Substring(0, ui.name.IndexOf("(Clone)"));
            return ui;
        }
        
    }
}
