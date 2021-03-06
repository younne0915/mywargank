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
   public class SkillHeal : SkillBase
   {
       public int HealValue;
       public int HealPercent;
        private static string _dataPath = "StaticData/SDSkillType/SkillHeal";
       private static Dictionary<string,SkillHeal> _data = new Dictionary<string,SkillHeal>();
       private static List<SkillHeal> _dataList = new List<SkillHeal>();
       public static List<SkillHeal> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SkillHeal> dataList = LitJson.JsonMapper.ToObject<List<SkillHeal>>(json);
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

        public static SkillHeal GetElement(string id)
       {
            SkillHeal val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}
