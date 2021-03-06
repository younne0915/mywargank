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
   public class SkillSummon : SkillBase
   {
       public string SummonCardId;
       public int SummonCount;
       public int ContinueTime;
       public FixedNum ForwardDistance;
       public int ContinueTimeRead;
        private static string _dataPath = "StaticData/SDSkillType/SkillSummon";
       private static Dictionary<string,SkillSummon> _data = new Dictionary<string,SkillSummon>();
       private static List<SkillSummon> _dataList = new List<SkillSummon>();
       public static List<SkillSummon> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SkillSummon> dataList = LitJson.JsonMapper.ToObject<List<SkillSummon>>(json);
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

        public static SkillSummon GetElement(string id)
       {
            SkillSummon val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}
