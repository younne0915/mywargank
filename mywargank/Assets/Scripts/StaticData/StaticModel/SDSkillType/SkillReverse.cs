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
   public class SkillReverse : SkillBase
   {
       public int ReverseMultiple;
       public string AddBuffId;
       public List<string> DamageType;
        private static string _dataPath = "StaticData/SDSkillType/SkillReverse";
       private static Dictionary<string,SkillReverse> _data = new Dictionary<string,SkillReverse>();
       private static List<SkillReverse> _dataList = new List<SkillReverse>();
       public static List<SkillReverse> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SkillReverse> dataList = LitJson.JsonMapper.ToObject<List<SkillReverse>>(json);
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

        public static SkillReverse GetElement(string id)
       {
            SkillReverse val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}
