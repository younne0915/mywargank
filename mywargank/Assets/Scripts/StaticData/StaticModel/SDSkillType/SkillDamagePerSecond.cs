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
   public class SkillDamagePerSecond : SkillBase
   {
       public int DamageValue;
       public string DamageType;
       public int ContinueTime;
       public string AddBuffId;
       public int CanOverlay;
       public int ContinueTimeRead;
        private static string _dataPath = "StaticData/SDSkillType/SkillDamagePerSecond";
       private static Dictionary<string,SkillDamagePerSecond> _data = new Dictionary<string,SkillDamagePerSecond>();
       private static List<SkillDamagePerSecond> _dataList = new List<SkillDamagePerSecond>();
       public static List<SkillDamagePerSecond> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SkillDamagePerSecond> dataList = LitJson.JsonMapper.ToObject<List<SkillDamagePerSecond>>(json);
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

        public static SkillDamagePerSecond GetElement(string id)
       {
            SkillDamagePerSecond val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}