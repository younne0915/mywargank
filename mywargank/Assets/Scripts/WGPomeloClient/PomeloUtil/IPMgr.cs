using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace WG
{
    public class IPMgr : Singleton<IPMgr>
    {

        public int pomeloPort = 3014;
        //public string pomeloIP = "60.205.125.242";
        public string pomeloIP = "121.42.36.142";

        private SDIPConfig _ipConfig;
        public void Init()
        {
            //PomeloStatusMgr.SetGateServerParam(pomeloIP, pomeloPort);
            _ipConfig = SDIPConfig.GetElement("Global");
            if(_ipConfig == null)
            {
                WGLogger.LogError(LogModule.Debug, "ip config is null");
            }
            else
            {
                PomeloStatusMgr.SetVersionServerParam(_ipConfig.PomeloIP, _ipConfig.PomeloPort);
                if (CheckGateServerCanUse())
                {
                    PomeloStatusMgr.SetGateServerParam(pomeloIP, pomeloPort);
                }
            }
        }

        public bool CheckGateServerCanUse()
        {
            string path = FileHelper.GetPath(Constant.GATE_FILE_NAME);
            if (FileHelper.FileExist(path))
            {
                string content = FileHelper.ReadFile(path);
                GateFile gateFile = LitJson.JsonMapper.ToObject<GateFile>(content);
                pomeloIP = gateFile.ip;
                pomeloPort = gateFile.port;
                System.DateTime time = gateFile.time;
                double dt = (System.DateTime.Now - time).TotalDays;
                if(dt >= 30)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
