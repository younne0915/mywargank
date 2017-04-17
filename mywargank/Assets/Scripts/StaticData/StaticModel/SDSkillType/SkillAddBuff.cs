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
   public class SkillAddBuff : SkillBase
   {
       public List<string> AddSkillId;
        private static string _dataPath = "StaticData/SDSkillType/SkillAddBuff";
       private static Dictionary<string,SkillAddBuff> _data = new Dictionary<string,SkillAddBuff>();
       private static List<SkillAddBuff> _dataList = new List<SkillAddBuff>();
       public static List<SkillAddBuff> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SkillAddBuff> dataList = LitJson.JsonMapper.ToObject<List<SkillAddBuff>>(json);
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

        public static SkillAddBuff GetElement(string id)
       {
            SkillAddBuff val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}
