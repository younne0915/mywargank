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
   public class SDSkillCard : StaticDataBase
   {
       public string Id;
       public string SkillId;
       public int Cost;
       public List<string> DisplaySkillProperty;
        private static string _dataPath = "StaticData/SDSkillCard";
       private static Dictionary<string,SDSkillCard> _data = new Dictionary<string,SDSkillCard>();
       private static List<SDSkillCard> _dataList = new List<SDSkillCard>();
       public static List<SDSkillCard> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SDSkillCard> dataList = LitJson.JsonMapper.ToObject<List<SDSkillCard>>(json);
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

        public static SDSkillCard GetElement(string id)
       {
            SDSkillCard val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}
