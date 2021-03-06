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
   public class SDPortrait : StaticDataBase
   {
       public string Id;
       public string PortraitIcon;
        private static string _dataPath = "StaticData/SDPortrait";
       private static Dictionary<string,SDPortrait> _data = new Dictionary<string,SDPortrait>();
       private static List<SDPortrait> _dataList = new List<SDPortrait>();
       public static List<SDPortrait> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SDPortrait> dataList = LitJson.JsonMapper.ToObject<List<SDPortrait>>(json);
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

        public static SDPortrait GetElement(string id)
       {
            SDPortrait val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}
