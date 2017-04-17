using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WG
{
    public enum GameStateType
    {
        Login,
        Main,
        Match,
        OnMatched,
        AllPlayersReady,
        Fight,
        Reconnecting,
    };

	public enum DisconnectError
	{
		ReMatch,
		GameOver,
		DupLogin,
		ReadyTimeout,
		InvalidRequest,
		ServerBusy,
		TokenError,
		CancelMatch,
		NoOneSendResult,
		InvalidGameOver,
		ServerNotRun,
		DisconnectGateServer = 100,
		Relogin = 101,
		Default,
	}

    public class GameState
    {

        public GameState(GameStateType type)
        {
            this._stateType = type;
        }

        protected GameStateType _stateType;
        public GameStateType stateType
        {
            get
            {
                return _stateType;
            }
        }
        public virtual void OnEnter(){}
        public virtual void OnExit() { }
        public virtual void OnUpdate() { }
        public virtual void PomeloFailed(string errorMsg)
        {
            Debug.Log("PomeloFailed : "+ errorMsg);
        }
        public void OnDisconnect(PomeloClientType type, string errorMsg)
        {
            //Debug.Log("type = "+type+",errorMsg = "+errorMsg);
            switch (type)
            {
                case PomeloClientType.PeaceServer:
                    OnPeaceDisconnect(errorMsg);
                    break;
                case PomeloClientType.FightServer:
                    OnFightDisconnect(errorMsg);
                    break;
            }
        }
        protected virtual void OnPeaceDisconnect(string errorMsg) { }
        protected virtual void OnFightDisconnect(string errorMsg) { }
    }

    public class StateMachineController
    {
        public static StateMachineController instance = null;

        private Dictionary<GameStateType, GameState> _gameStateDic = new Dictionary<GameStateType, GameState>();

        private GameState _curState;
        private GameState _nextState;

        //public GameState curState
        //{
        //    get { return _curState; }
        //}

        public StateMachineController()
        {
            instance = this;
            RegisterStateMachine();
        }

        private void RegisterStateMachine()
        {
            _gameStateDic[GameStateType.Login] = new LoginState(GameStateType.Login);
            _gameStateDic[GameStateType.Main] = new MainState(GameStateType.Main);
            _gameStateDic[GameStateType.Match] = new MatchState(GameStateType.Match);
            _gameStateDic[GameStateType.OnMatched] = new OnMatchedState(GameStateType.OnMatched);
            _gameStateDic[GameStateType.AllPlayersReady] = new AllPlayersReadyState(GameStateType.AllPlayersReady);
            _gameStateDic[GameStateType.Fight] = new FightState(GameStateType.Fight);
            _gameStateDic[GameStateType.Reconnecting] = new ReconnectingState(GameStateType.Reconnecting);
        }

        public void SetNextState(GameStateType type)
        {
            //if(_curState != null && type == _curState.stateType)
            //{
            //    //Debug.Log(LogType.Error, "state is same");
            //    return;
            //}
            _nextState = _gameStateDic[type];
        }

        public void Update()
        {
            if(_nextState != null)
            {
                ChangeState();
            }
            if(_curState!= null)
            {
                _curState.OnUpdate();
            }
        }

		private void ChangeState()
        {
            if(_curState != null)
            {
                _curState.OnExit();
            }
            _curState = _nextState;
            _nextState = null;
            _curState.OnEnter();
        }

        public void OnDisconnect(PomeloClientType type, string errorMsg)
        {
            _curState.OnDisconnect(type, errorMsg);
        }
    }
}
