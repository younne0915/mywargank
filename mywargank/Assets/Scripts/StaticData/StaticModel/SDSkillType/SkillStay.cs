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
   public class SkillStay : SkillBase
   {
       public int StayTime;
       public int SkillCD;
        private static string _dataPath = "StaticData/SDSkillType/SkillStay";
       private static Dictionary<string,SkillStay> _data = new Dictionary<string,SkillStay>();
       private static List<SkillStay> _dataList = new List<SkillStay>();
       public static List<SkillStay> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SkillStay> dataList = LitJson.JsonMapper.ToObject<List<SkillStay>>(json);
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

        public static SkillStay GetElement(string id)
       {
            SkillStay val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}