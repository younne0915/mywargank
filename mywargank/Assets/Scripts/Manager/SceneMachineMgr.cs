using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

namespace WG
{
    public class SceneMachineMgr
    {
        private static AsyncOperation _async = null;
        private static Action _callback = null;

        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public static void LoadSceneAsync(string sceneName, Action loadedCallback = null)
        {
            _callback = loadedCallback;
            _async = SceneManager.LoadSceneAsync(sceneName);
            CoroutineHelper.instance.StartCorotineBehavior(WaitForSceneLoaded());
        }

        private static IEnumerator WaitForSceneLoaded()
        {
            while(_async.progress < 0.99f)
            {
                yield return null;
            }
            if(_callback != null)
            {
                _callback();
            }
            _async = null;
        }
    }
}
