﻿namespace SplendorServer.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class User
    {
        #region Fields

        private readonly ChipState chipState = new ChipState();

        private readonly List<Card> ownedCards = new List<Card>();

        #endregion

        #region Public Properties

        public ChipState ChipState
        {
            get
            {
                var retVal = new ChipState
                                 {
                                     White = this.chipState.White + this.ownedCards.Count(o => o.Color == CardType.White), 
                                     Blue = this.chipState.Blue + this.ownedCards.Count(o => o.Color == CardType.Blue), 
                                     Green = this.chipState.Green + this.ownedCards.Count(o => o.Color == CardType.Green), 
                                     Red = this.chipState.Red + this.ownedCards.Count(o => o.Color == CardType.Red), 
                                     Black = this.chipState.Black + this.ownedCards.Count(o => o.Color == CardType.Black), 
                                     Gold = this.chipState.Gold
                                 };
                return retVal;
            }
        }

        public int VictoryPoints
        {
            get
            {
                return this.ownedCards.Sum(card => card.VictoryPoints);
            }
        }

        public string Name { get; set; }

        #endregion
    }
}