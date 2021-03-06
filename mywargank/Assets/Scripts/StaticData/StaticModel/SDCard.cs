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
   public class SDCard : StaticDataBase
   {
       public string Id;
       public int Level;
       public int CardType;
       public int Rarity;
       public string IconName;
       public int UpgradeCardCost;
       public int UpgradeGoldCost;
       public string Name;
       public string Describe;
       public string BriefDescribe;
       public string Portrait;
        private static string _dataPath = "StaticData/SDCard";
       private static Dictionary<string,SDCard> _data = new Dictionary<string,SDCard>();
       private static List<SDCard> _dataList = new List<SDCard>();
       public static List<SDCard> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SDCard> dataList = LitJson.JsonMapper.ToObject<List<SDCard>>(json);
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

        public static SDCard GetElement(string id)
       {
            SDCard val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}
