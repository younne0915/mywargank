using UnityEngine;
using System.Collections;

namespace WG
{

    public class UISoundHelper : MonoBehaviour
    {
        public UIPlaySound uiPlaySound;
        // Use this for initialization
        void Start()
        {
            uiPlaySound = GetComponent<UIPlaySound>();
        }

        private bool _lastSettingAudio = true;

        // Update is called once per frame
        void Update()
        {
            //if (SettingController.instance.settingModel.audioOn != _lastSettingAudio)
            //{
            //    _lastSettingAudio = SettingController.instance.settingModel.audioOn;
            //    uiPlaySound.enabled = _lastSettingAudio;
            //}            
        }
    }

}