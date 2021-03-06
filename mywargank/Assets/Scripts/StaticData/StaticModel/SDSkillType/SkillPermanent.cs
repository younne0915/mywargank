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
   public class SkillPermanent : SkillBase
   {
       public int AttackValue;
       public int AttackPercent;
       public int ArmorValue;
       public int AttackSpeedPercent;
       public int MoveSpeedPercent;
       public int HpRecoveryValue;
       public int HpRecoveryPercent;
       public string AddBuffId;
       public int SuckBloodRate;
       public int DodgeRate;
       public int HitRate;
        private static string _dataPath = "StaticData/SDSkillType/SkillPermanent";
       private static Dictionary<string,SkillPermanent> _data = new Dictionary<string,SkillPermanent>();
       private static List<SkillPermanent> _dataList = new List<SkillPermanent>();
       public static List<SkillPermanent> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SkillPermanent> dataList = LitJson.JsonMapper.ToObject<List<SkillPermanent>>(json);
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

        public static SkillPermanent GetElement(string id)
       {
            SkillPermanent val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}
