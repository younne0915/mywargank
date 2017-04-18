using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using MatchResponseHandler = Protocol.ServerResponseInterFace.Match.MatchHandler;

namespace WG
{
    public class MatchController : Singleton<MatchController>
    {
        private MatchUI _matchUI;
        private MatchModel _matchModel;

        public MatchController()
        {
            _matchModel = new MatchModel();
        }

        public void SetEmtime(int emtime)
        {
            _matchModel.SetEmtime(emtime);
        }

        public void OpenMatchUI(BaseUIWindow lastWindow)
        {
            if (_matchUI == null)
            {
                GameObject go = UIManager.CreateUI(Constant.UI_MATCH_PATH + MatchUI.MATCHUI_PREFAB_NAME);
                _matchUI = go.GetComponent<MatchUI>();
                _matchUI.Init(this);
            }
            else
            {
                _matchUI.ShowUI();
            }
            _matchUI.RefresUI();
            _lastWindow = lastWindow;
            _lastWindow.HideUI();
        }

        public void HideMatchUI()
        {
            _matchUI.HideUI();
        }

        public void SendCancleMatch()
        {
            PomeloMsgSender.SendCancleMatch(SendCancleMatchCallback);
        }

        private void SendCancleMatchCallback(MatchResponseHandler.CancelMatch response)
        {
            if (NetworkHelper.CheckPomeloCodeIsSuccess(response.code))
            {
                Debug.Log("SendCancleMatchCallback");
            }
            else
            {

            }
        }

        public override void Clear(bool clearInstance)
        {
            if(_matchUI != null)
            {
                _matchUI.DestroyUI();
            }

            base.Clear(clearInstance);
            //TODO 调用GC
        }
    }
}
