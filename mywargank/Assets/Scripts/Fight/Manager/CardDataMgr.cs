using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util;

namespace WG
{

    public enum CardType
    {
        HeroCard = 0,
        SkillCard,
    }

    public class CardDataMgr : Singleton<CardDataMgr>
    {
        private Dictionary<int, List<SDCard>> _allPlayerCards = new Dictionary<int, List<SDCard>>();
        private Dictionary<int, Dictionary<int, List<SDCard>>> _seatAllCards = new Dictionary<int, Dictionary<int, List<SDCard>>>();
        private Dictionary<int, Dictionary<int, SDCard>> _seatCards = new Dictionary<int, Dictionary<int, SDCard>>();
        //    private Dictionary<string, List<SDCard>> _heroCards = new Dictionary<string, List<SDCard>>();
        private Dictionary<int, List<SDCard>> _allPlayerSkillCards = new Dictionary<int, List<SDCard>>();
        private Dictionary<int, List<SDCard>> _allPlayerSkillCardsInPanel = new Dictionary<int, List<SDCard>>();

        private Dictionary<int, List<SDCard>> _allPlayerMercenaryCards = new Dictionary<int, List<SDCard>>();
        private Dictionary<int, List<SDCard>> _allPlayerMercenaryCardsInPanel = new Dictionary<int, List<SDCard>>();

        private Dictionary<int, List<SDCard>> _allPlayerCrystalCards = new Dictionary<int, List<SDCard>>();

        private Dictionary<int, List<SDCard>> _allPlayerDefaultHeroCards = new Dictionary<int, List<SDCard>>();

        public void CacheCardsForEachPlayer(int numberID, List<string> cards, List<string> battleCardsDefault)
        {
            InitCardLists(numberID);
            for (int i = 0; i < cards.Count; i++)
            {
                SDCard card = SDCard.GetElement(cards[i]);
                _allPlayerCards[numberID].Add(card);
                InitCardByType(numberID, card);
            }
            if (battleCardsDefault != null)
            {
                for (int i = 0; i < battleCardsDefault.Count; i++)
                {
                    SDCard card = SDCard.GetElement(battleCardsDefault[i]);
                    CardType cardType = ConvertHelper.ConvertToEnum<CardType>(card.CardType);
                    if (cardType == CardType.HeroCard)
                    {
                        _allPlayerDefaultHeroCards[numberID].Add(card);
                    }
                    else if (cardType == CardType.SkillCard)
                    {
                        _allPlayerSkillCardsInPanel[numberID].Add(card);
                    }
                }
            }
            IninSkillCardInPanel(numberID);
            InitMercenaryCardsInPanel(numberID);
        }

        void InitCardLists(int numberID)
        {
            _allPlayerCards[numberID] = new List<SDCard>();
            _allPlayerDefaultHeroCards[numberID] = new List<SDCard>();
            _allPlayerSkillCards[numberID] = new List<SDCard>();
            _allPlayerMercenaryCards[numberID] = new List<SDCard>();
            _seatAllCards[numberID] = new Dictionary<int, List<SDCard>>();
            _allPlayerSkillCardsInPanel[numberID] = new List<SDCard>();
            _allPlayerMercenaryCardsInPanel[numberID] = new List<SDCard>();
            _allPlayerCrystalCards[numberID] = new List<SDCard>();
        }

        void InitCardByType(int numberID, SDCard card)
        {
            if (card.CardType == (int)(CardType.HeroCard))
            {
                SDHeroCard heroCard = SDHeroCard.GetElement(card.Id);
                if (heroCard.Lansquenet == 0)
                {
                    if (heroCard.CardType == "crystal")
                    {
                        _allPlayerCrystalCards[numberID].Add(card);
                    }
                    else
                    {
                        InitCardSeat(numberID, card);
                    }
                }
                //else
                //{
                //    _allPlayerMercenaryCards[numberID].Add(card);
                //}
            }
            else if (card.CardType == (int)(CardType.SkillCard))
            {
                _allPlayerSkillCards[numberID].Add(card);
            }
        }

        void InitCardSeat(int numberID, SDCard card)
        {
            //if (card.Seat == HERO_CARD_SEAT)
            //{
            //    if (!_heroCards.ContainsKey(numberID))
            //    {
            //        _heroCards[numberID] = new List<SDCard>();
            //    }
            //    _heroCards[numberID].Add(card);
            //}
            //else
            {
                SDHeroCard heroCard = SDHeroCard.GetElement(card.Id);
                if (!_seatAllCards[numberID].ContainsKey(heroCard.Seat))
                {

                    _seatAllCards[numberID][heroCard.Seat] = new List<SDCard>();

                }
                _seatAllCards[numberID][heroCard.Seat].Add(card);
            }
        }

        void IninSkillCardInPanel(int numberID)
        {
            
        }

        void InitMercenaryCardsInPanel(int numberID)
        {
            List<string> mercenaryCardsIDs = FightManager.getInstance().sdBattle.LansquenetCardId;
            for (int i = 0; i < mercenaryCardsIDs.Count; i++)
            {
                SDCard card = SDCard.GetElement(mercenaryCardsIDs[i]);
                if (card != null)
                {
                    _allPlayerMercenaryCards[numberID].Add(card);
                    _allPlayerMercenaryCardsInPanel[numberID].Add(card);
                }
            }
        }

        public void InitPlayerCards()
        {
            for (int i = 0; i < PlayerMgr.getInstance().playerList.Count; i++)
            {
                InitDefaultCardSeat(PlayerMgr.getInstance().playerList[i].numberID);
            }
        }

        public void InitDefaultCardSeat(int numberID)
        {

        }
    }
}
