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
   public class SkillEjector : SkillBase
   {
       public int EjectorTimes;
       public FixedNum EjectorRange;
       public int DamageDamping;
       public int HealValue;
       public string EjectorRangeRead;
        private static string _dataPath = "StaticData/SDSkillType/SkillEjector";
       private static Dictionary<string,SkillEjector> _data = new Dictionary<string,SkillEjector>();
       private static List<SkillEjector> _dataList = new List<SkillEjector>();
       public static List<SkillEjector> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SkillEjector> dataList = LitJson.JsonMapper.ToObject<List<SkillEjector>>(json);
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

        public static SkillEjector GetElement(string id)
       {
            SkillEjector val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}
