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
   public class SDBattle : StaticDataBase
   {
       public string Id;
       public string Name;
       public string BattleGrade;
       public string SceneName;
       public string Scene;
       public int OpenWarFog;
       public List<int> Round;
       public List<int> SpawnTime;
       public int MaxRound;
       public int FirstChangeCost;
       public int ChangeCD;
       public int ChangeMaxTimes;
       public int MaxHp;
       public string GoldNumEffect;
       public int GoldOrigin;
       public int CrystalOrigin;
       public int LastRoundWaitTime;
       public List<int> GoldPerSecond;
       public List<int> MaxPopulation;
       public int HeroUnlockRound;
       public int ResolveIntegral;
       public int SaveHistory;
       public List<int> SubstrateA;
       public List<int> SubstrateB;
       public List<int> CrystalA;
       public List<int> CrystalB;
       public string SubstrateId;
       public int SpeedUpCD;
       public List<string> UserCardId;
       public List<string> LansquenetCardId;
       public string AIUser;
       public List<string> BattleFirstMusicID;
       public List<string> BattleSecondMusicID;
       public string ClimbTower;
       public string ScenePath;
       public string BattleType;
       public string CrystalCardId;
        private static string _dataPath = "StaticData/SDBattle";
       private static Dictionary<string,SDBattle> _data = new Dictionary<string,SDBattle>();
       private static List<SDBattle> _dataList = new List<SDBattle>();
       public static List<SDBattle> GetDataList()
       {
           return _dataList;
       }

       public override void LoadData()
       {
           string json = StaticDataLoader.GetText(_dataPath);
           List<SDBattle> dataList = LitJson.JsonMapper.ToObject<List<SDBattle>>(json);
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

        public static SDBattle GetElement(string id)
       {
            SDBattle val = null;
            _data.TryGetValue(id, out val);
            return val;
       }
   }
}
