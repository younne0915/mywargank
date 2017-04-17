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
   public class SDTaunt : StaticDataBase
   {
       public string Id;
       public string Word;
       public string Face;
       public string SDAudioResource;
        private static string _dataPath = "StaticData/SDTaunt";
       private static Dictionary<string,SDTaunt> _data = new Dictionary<string,SDTaunt>();
       private static List<SDTaunt> _dataList = new List<SDTaunt>();
       public static List<SDTaunt> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SDTaunt> dataList = LitJson.JsonMapper.ToObject<List<SDTaunt>>(json);
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

        public static SDTaunt GetElement(string id)
       {
            SDTaunt val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}