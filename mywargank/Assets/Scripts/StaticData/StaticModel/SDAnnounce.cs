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
   public class SDAnnounce : StaticDataBase
   {
       public string Id;
       public string Title;
       public string Content;
        private static string _dataPath = "StaticData/SDAnnounce";
       private static Dictionary<string,SDAnnounce> _data = new Dictionary<string,SDAnnounce>();
       private static List<SDAnnounce> _dataList = new List<SDAnnounce>();
       public static List<SDAnnounce> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SDAnnounce> dataList = LitJson.JsonMapper.ToObject<List<SDAnnounce>>(json);
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

        public static SDAnnounce GetElement(string id)
       {
            SDAnnounce val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}