﻿namespace SplendorCommonLibrary.Models
{
    #region

    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;

    using SplendorCommonLibrary.Helpers;

    #endregion

    public class Deck
    {
        #region Constants

        private const int VisibleCardsCount = 4;

        #endregion

        #region Fields

        private readonly IList<Aristocrate> allAristocrates;

        private readonly IList<Card> allCards;

        private readonly Game game;

        private IList<Card> cardsInBank;

        #endregion

        #region Constructors and Destructors

        public Deck(Game game, string deckFilePath, string aristocratesFilePath)
        {
            this.game = game;
            var deckFileText = GetTextFromFile(deckFilePath);

            this.allCards = JsonConvert.DeserializeObject<List<Card>>(deckFileText);

            var aristocratesFileText = GetTextFromFile(aristocratesFilePath);

            this.allAristocrates = JsonConvert.DeserializeObject<List<Aristocrate>>(aristocratesFileText);
        }

        #endregion

        #region Public Properties

        public IList<Aristocrate> AllAristocrates
        {
            get
            {
                return this.allAristocrates;
            }
        }

        public IList<Card> AllCards
        {
            get
            {
                return this.allCards;
            }
        }

        public IList<Aristocrate> AvailableAristocrates { get; private set; }

        public IDictionary<int, ICollection<Card>> AvailableCards
        {
            get
            {
                var retVal = new Dictionary<int, ICollection<Card>>();
                for (var i = 1; i <= 3; i++)
                {
                    retVal.Add(i, this.GetVisibleCardsOfTier(i));
                }

                return retVal;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void Initialize()
        {
            this.cardsInBank = this.allCards.Shuffle();
            var numberOfAristocrates = this.game.Players.Count + 1;
            this.AvailableAristocrates = this.allAristocrates.Shuffle().Take(numberOfAristocrates).ToList();
        }

        #endregion

        #region Methods

        private static string GetTextFromFile(string deckFilePath)
        {
            if (!File.Exists(deckFilePath))
            {
                throw new FileNotFoundException("Data file not found: " + deckFilePath);
            }

            var jsonString = File.ReadAllText(deckFilePath);
            return jsonString;
        }

        private ICollection<Card> GetVisibleCardsOfTier(int i)
        {
            return this.cardsInBank.Where(card => card.Tier == i).Take(VisibleCardsCount).ToArray();
        }

        #endregion
    }
}