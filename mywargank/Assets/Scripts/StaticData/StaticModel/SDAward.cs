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
   public class SDAward : StaticDataBase
   {
       public string Id;
       public int Type;
       public List<string> Detail;
       public int DescriptionId;
        private static string _dataPath = "StaticData/SDAward";
       private static Dictionary<string,SDAward> _data = new Dictionary<string,SDAward>();
       private static List<SDAward> _dataList = new List<SDAward>();
       public static List<SDAward> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SDAward> dataList = LitJson.JsonMapper.ToObject<List<SDAward>>(json);
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

        public static SDAward GetElement(string id)
       {
            SDAward val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}
