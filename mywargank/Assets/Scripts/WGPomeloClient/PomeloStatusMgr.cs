using Pomelo.DotNetClient;
using Protocol;
using Simon.CustomSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WG
{
    public class GateFile
    {
        public string ip;
        public int port;
        public DateTime time;
    }

    public enum PomeloClientType
    {
        FightServer,
        PeaceServer,
    }

    public enum PomeloClientState
    {
        Connected,
        Disconnected
    }

    public class PomeloResult
    {
        public int code;
        public string errorMsg;

        public PomeloResult(int code, string errorMsg)
        {
            this.code = code;
            this.errorMsg = errorMsg;
        }
    }

    

    public class PomeloStatusMgr
    {
        private static string _gateIP;
        private static int _gatePort;
        private static string _versionIP;
        private static int _versionPort;

        private static Dictionary<PomeloClientType, WGPomeloClient> _wgPolemoClientDic = new Dictionary<PomeloClientType, WGPomeloClient>();

        public static PomeloClientState GetParticPomeloClientState(PomeloClientType type)
        {
            if (_wgPolemoClientDic.ContainsKey(type))
            {
                return _wgPolemoClientDic[type].state;
            }
            return PomeloClientState.Disconnected;
        }

        public static WGPomeloClient GetParticPomeloClient(PomeloClientType type)
        {
            return _wgPolemoClientDic[type];
        }

        public static void SetVersionServerParam(string host, int port)
        {
            _versionIP = host;
            _versionPort = port;
        }

        public static void SetGateServerParam(string gateIP, int gatePort)
        {
            _gateIP = gateIP;
            _gatePort = gatePort;
            foreach (WGPomeloClient pc in _wgPolemoClientDic.Values)
            {
                pc.SetGateServerParam(_gateIP, _gatePort);
            }
        }

        public static bool ConnectServer(PomeloClientType type, Action<PomeloResult> callback)
        {
            if (!_wgPolemoClientDic.ContainsKey(type))
            {
                if(type == PomeloClientType.PeaceServer)
                {
                    _wgPolemoClientDic[type] = new PeacePomeloClient(type);
                }
                else if(type == PomeloClientType.FightServer)
                {
                    _wgPolemoClientDic[type] = new FightPomeloClient(type);
                }
                _wgPolemoClientDic[type].SerVersionParam(_versionIP, _versionPort);
                _wgPolemoClientDic[type].SetGateServerParam(_gateIP, _gatePort);
            }
            return _wgPolemoClientDic[type].ConnectServer(callback);
        }

        public static void SendMsg(PomeloClientType type, string route, SimonMsg msg, Action<object> action)
        {
            WGPomeloClient wc = _wgPolemoClientDic[type];
            if(wc != null)
            {
                wc.SendMsg(route, msg, action);
            }
        }

        public static bool IsServerConnected()
        {
            if(GetParticPomeloClientState(PomeloClientType.PeaceServer) == PomeloClientState.Connected)
            {
                return true;
            }
            return false;
        }

        public static void DisconnectServer(PomeloClientType type, string reason)
        {
            if (_wgPolemoClientDic.ContainsKey(type))
            {
                _wgPolemoClientDic[type].DisConnect(reason);
            }
        }

        public static void DisconnectServerByPomelo(PomeloClientType type, string reason)
        {
            if (_wgPolemoClientDic.ContainsKey(type))
            {
                _wgPolemoClientDic[type].DisConnectByPomelo(reason);
            }
        }

        public static void ClearConnectorIP()
        {
            foreach (WGPomeloClient pc in _wgPolemoClientDic.Values)
            {
                pc.ClearConnectorIP();
            }
        }
    }
}
