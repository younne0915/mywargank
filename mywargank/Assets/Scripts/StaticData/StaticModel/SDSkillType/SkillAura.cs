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
   public class SkillAura : SkillBase
   {
       public int AttackValue;
       public int AttackPercent;
       public int ArmorValue;
       public int AttackSpeedPercent;
       public int MoveSpeedPercent;
       public int HpRecoveryValue;
       public int HpRecoveryPercent;
       public int InfluenceRange;
       public string AddBuffId;
       public int SuckBloodRate;
       public int ManaRecoveryValue;
       public int DodgeRate;
       public int HitRate;
       public int MagicReduceRate;
       public int ContinueTime;
       public string AuraEffectBuffId;
       public string ExtraSkill;
       public int AttackSpeedAbs;
       public int HpRecoveryPercentAbs;
       public int HpRecoveryValueAbs;
       public int ArmorValueAbs;
       public int AttackValueAbs;
       public string ContinueTimeRead;
       public string AttackPercentAbs;
       public string MoveSpeedPercentAbs;
       public string AttackSpeedPercentAbs;
        private static string _dataPath = "StaticData/SDSkillType/SkillAura";
       private static Dictionary<string,SkillAura> _data = new Dictionary<string,SkillAura>();
       private static List<SkillAura> _dataList = new List<SkillAura>();
       public static List<SkillAura> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SkillAura> dataList = LitJson.JsonMapper.ToObject<List<SkillAura>>(json);
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

        public static SkillAura GetElement(string id)
       {
            SkillAura val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}
