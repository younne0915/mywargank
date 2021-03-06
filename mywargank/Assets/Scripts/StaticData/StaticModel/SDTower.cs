// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SkillGenerator tool
//      Version: 1.0.0.0

//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

using Util;
using System.Collections;
using System.Collections.Generic;
namespace WG
{
   public class SDTower : StaticDataBase
   {
       public string Id;
       public int FirstDeployTime;
       public int DeployTime;
       public int SalaryOrigin;
       public int SalaryRise;
       public List<string> WaveList;
        private static string _dataPath = "StaticData/SDTower";
       private static Dictionary<string,SDTower> _data = new Dictionary<string,SDTower>();
       private static List<SDTower> _dataList = new List<SDTower>();
       public static List<SDTower> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SDTower> dataList = LitJson.JsonMapper.ToObject<List<SDTower>>(json);
           for(int i = 0; i < dataList.Count; i++)
           {
               _data.Add(dataList[i].Id, dataList[i]);
               _dataList.Add(dataList[i]);
           }
       }

       public override void Clear()
       {
           base.Clear();
           _dataList.Clear();
           _data.Clear();
       }

        public static SDTower GetElement(string id)
       {
            SDTower val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}
