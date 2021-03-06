// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by ProtocalGenerator tool
//      Version: 1.0.0.0
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Protocol
{

    public class ClassFullName
    {
        public static string GetClassFullName(string route)
        {
            switch (route)
            {
case "gate.gateHandler.getGameConnector":
return "Protocol.ServerResponseInterFace+Gate+GateHandler+GetGameConnector";

case "gate.gateHandler.getSceneConnector":
return "Protocol.ServerResponseInterFace+Gate+GateHandler+GetSceneConnector";

case "gameConnector.entryHandler.enterMatchQueue":
return "Protocol.ServerResponseInterFace+GameConnector+EntryHandler+EnterMatchQueue";

case "gameConnector.entryHandler.reconnectFightArea":
return "Protocol.ServerResponseInterFace+GameConnector+EntryHandler+ReconnectFightArea";

case "gameConnector.entryHandler.enterFightAreaAfterInvited":
return "Protocol.ServerResponseInterFace+GameConnector+EntryHandler+EnterFightAreaAfterInvited";

case "fight.fightHandler.getActionsHistory":
return "Protocol.ServerResponseInterFace+Fight+FightHandler+GetActionsHistory";

case "fight.fightHandler.recallOnAllPlayersReadyMessage":
return "Protocol.ServerResponseInterFace+Fight+FightHandler+RecallOnAllPlayersReadyMessage";

case "fight.fightHandler.fight":
return "Protocol.ServerResponseInterFace+Fight+FightHandler+Fight";

case "fight.fightHandler.ready":
return "Protocol.ServerResponseInterFace+Fight+FightHandler+Ready";

case "fight.fightHandler.startFight":
return "Protocol.ServerResponseInterFace+Fight+FightHandler+StartFight";

case "fight.fightHandler.endFight":
return "Protocol.ServerResponseInterFace+Fight+FightHandler+EndFight";

case "match.matchHandler.cancelMatch":
return "Protocol.ServerResponseInterFace+Match+MatchHandler+CancelMatch";

case "scene.friendHandler.replyFriendGameInvitation":
return "Protocol.ServerResponseInterFace+Scene+FriendHandler+ReplyFriendGameInvitation";

case "scene.friendHandler.cancelFriendGameInvitation":
return "Protocol.ServerResponseInterFace+Scene+FriendHandler+CancelFriendGameInvitation";

case "scene.friendHandler.chat":
return "Protocol.ServerResponseInterFace+Scene+FriendHandler+Chat";

case "scene.friendHandler.sendFriendGameInvitation":
return "Protocol.ServerResponseInterFace+Scene+FriendHandler+SendFriendGameInvitation";

case "scene.friendHandler.sendAddFriendMsg":
return "Protocol.ServerResponseInterFace+Scene+FriendHandler+SendAddFriendMsg";

case "scene.friendHandler.initFriendsList":
return "Protocol.ServerResponseInterFace+Scene+FriendHandler+InitFriendsList";

case "scene.friendHandler.replyAddFriendInvitation":
return "Protocol.ServerResponseInterFace+Scene+FriendHandler+ReplyAddFriendInvitation";

case "sceneConnector.boxHandler.startUnlockBox":
return "Protocol.ServerResponseInterFace+SceneConnector+BoxHandler+StartUnlockBox";

case "sceneConnector.userHandler.login":
return "Protocol.ServerResponseInterFace+SceneConnector+UserHandler+Login";

case "sceneConnector.userHandler.register":
return "Protocol.ServerResponseInterFace+SceneConnector+UserHandler+Register";

case "sceneConnector.userHandler.userDetail":
return "Protocol.ServerResponseInterFace+SceneConnector+UserHandler+UserDetail";

case "sceneConnector.userHandler.updateNickname":
return "Protocol.ServerResponseInterFace+SceneConnector+UserHandler+UpdateNickname";

case "sceneConnector.userHandler.updatePortrait":
return "Protocol.ServerResponseInterFace+SceneConnector+UserHandler+UpdatePortrait";

case "sceneConnector.userHandler.updatePassword":
return "Protocol.ServerResponseInterFace+SceneConnector+UserHandler+UpdatePassword";

case "sceneConnector.guideHandler.finishGuideStep":
return "Protocol.ServerResponseInterFace+SceneConnector+GuideHandler+FinishGuideStep";

case "sceneConnector.trainingHandler.finishTrainingStep":
return "Protocol.ServerResponseInterFace+SceneConnector+TrainingHandler+FinishTrainingStep";

case "sceneConnector.friendHandler.search":
return "Protocol.ServerResponseInterFace+SceneConnector+FriendHandler+Search";

case "sceneConnector.friendHandler.delete":
return "Protocol.ServerResponseInterFace+SceneConnector+FriendHandler+Delete";

case "sceneConnector.shopHandler.getShopList":
return "Protocol.ServerResponseInterFace+SceneConnector+ShopHandler+GetShopList";

case "sceneConnector.shopHandler.refreshSellCardList":
return "Protocol.ServerResponseInterFace+SceneConnector+ShopHandler+RefreshSellCardList";

case "sceneConnector.shopHandler.buyShopCard":
return "Protocol.ServerResponseInterFace+SceneConnector+ShopHandler+BuyShopCard";

case "sceneConnector.shopHandler.buyShopBox":
return "Protocol.ServerResponseInterFace+SceneConnector+ShopHandler+BuyShopBox";

case "sceneConnector.shopHandler.buyShopGold":
return "Protocol.ServerResponseInterFace+SceneConnector+ShopHandler+BuyShopGold";

case "sceneConnector.shopHandler.buyShopJewel":
return "Protocol.ServerResponseInterFace+SceneConnector+ShopHandler+BuyShopJewel";

case "sceneConnector.tavernHandler.getList":
return "Protocol.ServerResponseInterFace+SceneConnector+TavernHandler+GetList";

case "sceneConnector.tavernHandler.recruit":
return "Protocol.ServerResponseInterFace+SceneConnector+TavernHandler+Recruit";

case "sceneConnector.signInHandler.getList":
return "Protocol.ServerResponseInterFace+SceneConnector+SignInHandler+GetList";

case "sceneConnector.signInHandler.signInToday":
return "Protocol.ServerResponseInterFace+SceneConnector+SignInHandler+SignInToday";

case "sceneConnector.sceneHandler.main":
return "Protocol.ServerResponseInterFace+SceneConnector+SceneHandler+Main";

case "sceneConnector.cardHandler.cardList":
return "Protocol.ServerResponseInterFace+SceneConnector+CardHandler+CardList";

case "sceneConnector.cardHandler.handleRedundantCard":
return "Protocol.ServerResponseInterFace+SceneConnector+CardHandler+HandleRedundantCard";

case "sceneConnector.cardHandler.setBattleCardGroup":
return "Protocol.ServerResponseInterFace+SceneConnector+CardHandler+SetBattleCardGroup";

case "sceneConnector.cardHandler.getBattleCardGroup":
return "Protocol.ServerResponseInterFace+SceneConnector+CardHandler+GetBattleCardGroup";

case "sceneConnector.cardHandler.upgrade":
return "Protocol.ServerResponseInterFace+SceneConnector+CardHandler+Upgrade";

case "sceneConnector.boxHandler.boxList":
return "Protocol.ServerResponseInterFace+SceneConnector+BoxHandler+BoxList";

case "sceneConnector.rankHandler.rankingList":
return "Protocol.ServerResponseInterFace+SceneConnector+RankHandler+RankingList";

case "sceneConnector.historyHandler.get1V1Record":
return "Protocol.ServerResponseInterFace+SceneConnector+HistoryHandler+Get1V1Record";

case "sceneConnector.historyHandler.getHistory1V1List":
return "Protocol.ServerResponseInterFace+SceneConnector+HistoryHandler+GetHistory1V1List";

case "sceneConnector.boxHandler.openBox":
return "Protocol.ServerResponseInterFace+SceneConnector+BoxHandler+OpenBox";

case "sceneConnector.achieveHandler.getAchievementList":
return "Protocol.ServerResponseInterFace+SceneConnector+AchieveHandler+GetAchievementList";

case "sceneConnector.achieveHandler.finishAchievement":
return "Protocol.ServerResponseInterFace+SceneConnector+AchieveHandler+FinishAchievement";

case "sceneConnector.taskHandler.getTaskList":
return "Protocol.ServerResponseInterFace+SceneConnector+TaskHandler+GetTaskList";

case "sceneConnector.taskHandler.finishTask":
return "Protocol.ServerResponseInterFace+SceneConnector+TaskHandler+FinishTask";

case "sceneConnector.towerHandler.getRestTowerCount":
return "Protocol.ServerResponseInterFace+SceneConnector+TowerHandler+GetRestTowerCount";

case "sceneConnector.towerHandler.finishTowerWave":
return "Protocol.ServerResponseInterFace+SceneConnector+TowerHandler+FinishTowerWave";

case "scene.towerHandler.startTower":
return "Protocol.ServerResponseInterFace+Scene+TowerHandler+StartTower";

case "scene.towerHandler.endTower":
return "Protocol.ServerResponseInterFace+Scene+TowerHandler+EndTower";

case "sceneConnector.mailHandler.getMailList":
return "Protocol.ServerResponseInterFace+SceneConnector+MailHandler+GetMailList";

case "sceneConnector.mailHandler.readMail":
return "Protocol.ServerResponseInterFace+SceneConnector+MailHandler+ReadMail";

case "sceneConnector.mailHandler.getMailAward":
return "Protocol.ServerResponseInterFace+SceneConnector+MailHandler+GetMailAward";

case "sceneConnector.virusHandler.getVirusCode":
return "Protocol.ServerResponseInterFace+SceneConnector+VirusHandler+GetVirusCode";

case "sceneConnector.virusHandler.getRecruitAward":
return "Protocol.ServerResponseInterFace+SceneConnector+VirusHandler+GetRecruitAward";

case "scene.virusHandler.infectVirus":
return "Protocol.ServerResponseInterFace+Scene+VirusHandler+InfectVirus";

case "sceneConnector.virusHandler.getVirusList":
return "Protocol.ServerResponseInterFace+SceneConnector+VirusHandler+GetVirusList";

case "sceneConnector.virusHandler.clearVirus":
return "Protocol.ServerResponseInterFace+SceneConnector+VirusHandler+ClearVirus";

case "sceneConnector.quickLoginHandler.verifyDeviceVersion":
return "Protocol.ServerResponseInterFace+SceneConnector+QuickLoginHandler+VerifyDeviceVersion";

case "sceneConnector.quickLoginHandler.verifyGameCenterToken":
return "Protocol.ServerResponseInterFace+SceneConnector+QuickLoginHandler+VerifyGameCenterToken";

case "sceneConnector.quickLoginHandler.appleLoginWithUniqueID":
return "Protocol.ServerResponseInterFace+SceneConnector+QuickLoginHandler+AppleLoginWithUniqueID";

case "sceneConnector.feedbackHandler.getRestFeedbackCountToday":
return "Protocol.ServerResponseInterFace+SceneConnector+FeedbackHandler+GetRestFeedbackCountToday";

case "sceneConnector.feedbackHandler.feedback":
return "Protocol.ServerResponseInterFace+SceneConnector+FeedbackHandler+Feedback";

case "version.versionHandler.getGateServer":
return "Protocol.ServerResponseInterFace+Version+VersionHandler+GetGateServer";

case "onKeyFrame":
return "Protocol.ServerPushInterface+OnKeyFrame";

case "onAllPlayersReady":
return "Protocol.ServerPushInterface+OnAllPlayersReady";

case "onGameOver":
return "Protocol.ServerPushInterface+OnGameOver";

case "onAdd":
return "Protocol.ServerPushInterface+OnAdd";

case "onLeave":
return "Protocol.ServerPushInterface+OnLeave";

case "onMatched":
return "Protocol.ServerPushInterface+OnMatched";

case "onMainStateChanged":
return "Protocol.ServerPushInterface+OnMainStateChanged";

case "onChat":
return "Protocol.ServerPushInterface+OnChat";

case "onAddedFriend":
return "Protocol.ServerPushInterface+OnAddedFriend";

case "onReceiveAddFriendInvitation":
return "Protocol.ServerPushInterface+OnReceiveAddFriendInvitation";

case "onReceiveReplyAddFriendInvitation":
return "Protocol.ServerPushInterface+OnReceiveReplyAddFriendInvitation";

case "onReceiveFriendGameInvitation":
return "Protocol.ServerPushInterface+OnReceiveFriendGameInvitation";

case "onReceiveCancelFriendGameInvitation":
return "Protocol.ServerPushInterface+OnReceiveCancelFriendGameInvitation";

case "onReceiveFinishVirusRecruit":
return "Protocol.ServerPushInterface+OnReceiveFinishVirusRecruit";

default:
return string.Empty;
    }
    }
    }

}
