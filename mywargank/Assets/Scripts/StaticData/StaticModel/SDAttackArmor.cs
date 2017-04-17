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
   public class SDAttackArmor : StaticDataBase
   {
       public string Id;
       public FixedNum normal;
       public FixedNum puncture;
       public FixedNum magic;
       public FixedNum thump;
       public FixedNum holy;
        private static string _dataPath = "StaticData/SDAttackArmor";
       private static Dictionary<string,SDAttackArmor> _data = new Dictionary<string,SDAttackArmor>();
       private static List<SDAttackArmor> _dataList = new List<SDAttackArmor>();
       public static List<SDAttackArmor> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SDAttackArmor> dataList = LitJson.JsonMapper.ToObject<List<SDAttackArmor>>(json);
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

        public static SDAttackArmor GetElement(string id)
       {
            SDAttackArmor val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}