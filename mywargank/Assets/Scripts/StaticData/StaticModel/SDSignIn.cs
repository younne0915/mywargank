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
   public class SDSignIn : StaticDataBase
   {
       public string Id;
       public int Month;
       public int Date;
       public List<string> Award;
       public int DoubleVipLevel;
       public string HeroIcon;
       public string HeroName;
       public string HeroDescription;
        private static string _dataPath = "StaticData/SDSignIn";
       private static Dictionary<string,SDSignIn> _data = new Dictionary<string,SDSignIn>();
       private static List<SDSignIn> _dataList = new List<SDSignIn>();
       public static List<SDSignIn> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SDSignIn> dataList = LitJson.JsonMapper.ToObject<List<SDSignIn>>(json);
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

        public static SDSignIn GetElement(string id)
       {
            SDSignIn val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}
