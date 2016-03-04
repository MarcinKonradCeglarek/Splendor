﻿namespace SplendorCore.Models
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using SplendorCore.Helpers;

    #endregion

    [DataContract]
    public class Card
    {
        #region Constructors and Destructors

        public Card()
        {
            this.Id = Guid.NewGuid();
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        public Color Color { get; set; }

        [DataMember]
        [JsonConverter(typeof(CardCostConverter))]
        public Chips Cost { get; set; }

        public Guid Id { get; set; }

        [DataMember]
        public int Tier { get; set; }

        [DataMember]
        public int VictoryPoints { get; set; }

        public IList<KeyValuePair<Color, int>> VisibleCost
        {
            get
            {
                return this.Cost.Where(o => o.Value > 0).OrderBy(o => o.Key).ToList();
            }
        }

        #endregion

        #region Public Methods and Operators

        public bool CanBuy(Chips chips)
        {
            var result = chips - this.Cost;
            var missingChips = 0;
            if (result.Black < 0)
            {
                missingChips -= result.Black;
            }

            if (result.Blue < 0)
            {
                missingChips -= result.Blue;
            }

            if (result.Green < 0)
            {
                missingChips -= result.Green;
            }

            if (result.Red < 0)
            {
                missingChips -= result.Red;
            }

            if (result.White < 0)
            {
                missingChips -= result.White;
            }

            return missingChips <= chips.Gold;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1} ({2} pts.)", this.RomanTier(), this.Color, this.VictoryPoints);
        }

        #endregion

        #region Methods

        private string RomanTier()
        {
            switch (this.Tier)
            {
                case 1:
                    return "I";
                case 2:
                    return "II";
                case 3:
                    return "III";
                default:
                    throw new NotSupportedException();
            }
        }

        #endregion
    }
}