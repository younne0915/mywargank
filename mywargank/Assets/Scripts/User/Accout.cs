using Protocol;
namespace WG
{
    public struct User
    {
        public string nickName;
        public int numberID;

        public User(string nickName, int numberID)
        {
            this.nickName = nickName;
            this.numberID = numberID;
        }
    }

    public class UserDetail
    {
        private string _nickName;
        public string nickName
        {
            get { return _nickName; }
        }

        private int _numberID;
        public int numberID
        {
            get { return _numberID; }
        }

        private int _portrait;
        public int portrait
        {
            get { return _portrait; }
        }

        public UserDetail(string nickName, int numberID, int portrait)
        {
            _nickName = nickName;
            _numberID = numberID;
            _portrait = portrait;
        }
    }
    public static class Accout
    {
        private static string _token;
        public static string token
        {
            get { return _token; }
        }

        public static void SetToken(string token)
        {
            _token = token;
        }

        private static int _numberID;
        public static int numberID
        {
            get { return _numberID; }
        }

        private static string _userName;
        public static string userName
        {
            set { _userName = value; }
            get { return _userName; }
        }

        private static string _pwd;
        public static string pwd
        {
            set { _pwd = value; }
            get { return _pwd; }
        }

        private static string _nickName = "...";
        public static string nickName
        {
            get { return _nickName; }
        }

        private static int _diamond;
        public static int diamond
        {
            get { return _diamond; }
        }

        private static int _gold;
        public static int gold
        {
            get { return _gold; }
        }

        private static int _grade;
        public static int grade
        {
            get { return _grade; }
        }

        private static int _integral;
        public static int intergral
        {
            get { return _integral; }
        }

        private static int _portrait = 1;
        public static int portrait
        {
            get { return _portrait; }
        }

        public static User _user;
        public static User user
        {
            get { return _user; }
        }

        public static bool isGuideStepFinish
        {
            //TODO
            //get { return string.IsNullOrEmpty(_guideStep) || _guideStep == "-1"; }
            get
            {
                //if (SDGuide.GetElement(_guideStep) == null)
                //{
                //    return true;
                //}
                return false;
            }
        }

        private static string _guideStep = "-1";
        public static string guideStep
        {
            //TODO
            // get { return "-1"; }
            get { return _guideStep; }
        }

        public static string guideName
        {
            get
            {
                //SDGuide sdGuide = SDGuide.GetElement(_guideStep);
                //if (sdGuide == null)
                //{
                //    return "-1";
                //}
                //else
                //{
                //    return sdGuide.GuideName;
                //}
                return "-1";
            }
        }

        public static void SetGuideStep(string nextGuideStep)
        {
            //TODO
            _guideStep = nextGuideStep;
            //  _guideStep = "-1";
        }

        private static int _nextTraining = 1;
        public static int nextTraining
        {
            get
            {
                return _nextTraining;
            }
        }
        public static void SetNextTraining(int nextTraining)
        {
            _nextTraining = nextTraining;
        }

        public static void Rename(string nickName)
        {
            _nickName = nickName;
            _user.nickName = nickName;
        }

        private static int _prepareRaiseDiamond;
        public static int prepareRaiseDiamond
        {
            get { return _prepareRaiseDiamond; }
        }
        public static void PrepareRaiseDiamond(int diamond)
        {
            _prepareRaiseDiamond = diamond;
        }

        public static void ActullayRaiseDiamond()
        {
            if (_prepareRaiseDiamond != 0)
            {
                _prepareRaiseDiamond = 0;
            }
        }

        public static void RaiseDiamond(int count)
        {
            _diamond += count;
            if (OnFlyDiamondEffect != null)
            {
                OnFlyDiamondEffect();
            }
            if (OnDiamondChanged != null)
            {
                OnDiamondChanged();
            }
        }

        public static void ReduceDiamond(int count)
        {
            _diamond -= count;
            if (OnDiamondChanged != null)
            {
                OnDiamondChanged();
            }
        }

        public static void SetGrade(int grade)
        {
            _grade = grade;
        }

        public static void SetIntergral(int intergral)
        {
            _integral = intergral;
        }


        private static int _prepareRaiseGold;
        public static int prepareRaiseGold
        {
            get { return _prepareRaiseGold; }
        }
        public static void PrepareRaiseGold(int gold)
        {
            _prepareRaiseGold = gold;
        }

        public static void ActullayRaiseGold()
        {
            if (_prepareRaiseGold != 0)
            {
                _prepareRaiseGold = 0;
            }
        }

        public static void RaiseGold(int gold)
        {
            _gold += gold;
            if (OnFlyGoldEffect != null)
            {
                OnFlyGoldEffect();
            }
            if (OnGoldChanged != null)
            {
                OnGoldChanged();
            }
        }

        public static System.Action OnDiamondChanged = null;
        public static System.Action OnGoldChanged = null;
        public static System.Action OnFlyGoldEffect = null;
        public static System.Action OnFlyDiamondEffect = null;
        public static void ReduceGold(int gold)
        {
            _gold -= gold;
            if (_gold <= 0) _gold = 0;
            if (OnGoldChanged != null)
            {
                OnGoldChanged();
            }
        }

        public static void SetGoldPortrait(int portrait)
        {
            _portrait = portrait;
        }

        /*
        public static void SetUserGameInfo(Protocol.HTTPResponse.Scene.Main.UserGameInfo userGameInfo)
        {            
            _numberID = userGameInfo.numberID;
            _nickName = userGameInfo.nickname;
            _portrait = userGameInfo.portrait;
            _integral = userGameInfo.integral;
            _grade = userGameInfo.grade;
            _diamond = userGameInfo.jewel;
            _gold = userGameInfo.gold;
            _guideStep = userGameInfo.nextGuideStep;
            _user = new User(_userName, _nickName, _numberID);
        }*/

        public static void SetUserGameInfo(LoginData data)
        {
            _numberID = data.numberID;
            _nickName = data.nickname;
            _portrait = data.portrait;
            _integral = data.integral;
            _grade = data.grade;
            _diamond = data.jewel;
            _gold = data.gold;
            _guideStep = data.nextGuideStep.ToString();
            _nextTraining = data.nextTrainingStep;
            _user = new User(_nickName, _numberID);
        }

        public static bool CheckIsDiamondEnough(int diamond)
        {
            return _diamond >= diamond;
        }

        public static bool CheckIsGoldEnough(int gold)
        {
            return _gold >= gold;
        }
    }
}