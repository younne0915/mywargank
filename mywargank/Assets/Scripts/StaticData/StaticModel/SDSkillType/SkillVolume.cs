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
   public class SkillVolume : SkillBase
   {
       public int VulumeCount;
       public string VolumeCardID;
        private static string _dataPath = "StaticData/SDSkillType/SkillVolume";
       private static Dictionary<string,SkillVolume> _data = new Dictionary<string,SkillVolume>();
       private static List<SkillVolume> _dataList = new List<SkillVolume>();
       public static List<SkillVolume> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SkillVolume> dataList = LitJson.JsonMapper.ToObject<List<SkillVolume>>(json);
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

        public static SkillVolume GetElement(string id)
       {
            SkillVolume val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}
