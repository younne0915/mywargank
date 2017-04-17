using LitJson;
using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UserReqsponseHandler = Protocol.ServerResponseInterFace.SceneConnector.UserHandler;

namespace WG
{
    public class LoginController : BaseUIController
    {
        public static LoginController instance = null;

        private LoginModel _loginModel;
        private LoginUI _loginUI;

        public LoginController()
        {
            instance = this;
            _loginModel = new LoginModel(this);
        }

        public void OpenLoginUI()
        {
            if(_loginUI == null)
            {
                GameObject go = UIManager.CreateUI(Constant.UI_LOGIN_PATH + LoginUI.LOGINUI_PREFAB_NAME);
                _loginUI = go.GetComponent<LoginUI>();
                _loginUI.Init(this);
            }
            else
            {
                _loginUI.ShowUI();
            }
        }

        public void SendLogin()
        {
            PomeloMsgSender.SendLogin("chao45", AES128Helper.getMD5("123"),Constant.PLATFORM, SendLoginCallback);
        }

        public void SendLoginCallback(UserReqsponseHandler.Login response)
        {
            if (NetworkHelper.CheckPomeloCodeIsSuccess(response.code))
            {
                LoginData loginData = JsonMapper.ToObject<LoginData>(response.userBasicInfoStr);
                SetUserGameInfo(loginData);
                StateMachineController.instance.SetNextState(GameStateType.Main);
            }
            else
            {
                Debug.Log("login failed");
            }
        }

        public static void SetUserGameInfo(LoginData data)
        {
            Accout.SetUserGameInfo(data);
            PeaceServerTimeMgr.instance.serverTime = long.Parse(data.serverTime);
        }

        public override void Clear(bool clearInstance)
        {
            base.Clear(clearInstance);
            if(_loginUI != null)
            {
                _loginUI.ClearUI();
            }
            if (clearInstance)
            {
                instance = null;

                //TODO 调用GC
            }
        }
    }
}
