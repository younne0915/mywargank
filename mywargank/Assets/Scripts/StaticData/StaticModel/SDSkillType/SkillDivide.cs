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
   public class SkillDivide : SkillBase
   {
       public FixedNum DivideRange;
       public int MaxEnemyCount;
       public int ExtraDamage;
       public string DamageType;
       public string LightningEffect;
        private static string _dataPath = "StaticData/SDSkillType/SkillDivide";
       private static Dictionary<string,SkillDivide> _data = new Dictionary<string,SkillDivide>();
       private static List<SkillDivide> _dataList = new List<SkillDivide>();
       public static List<SkillDivide> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SkillDivide> dataList = LitJson.JsonMapper.ToObject<List<SkillDivide>>(json);
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

        public static SkillDivide GetElement(string id)
       {
            SkillDivide val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}
