﻿namespace SplendorCore.Models.Exceptions.OperationExceptions
{
    #region

    using SplendorCore.Models.Exceptions.AbstractExceptions;

    #endregion

    public class PurchaseCardException : SplendorCardOperationException
    {
        #region Constructors and Destructors

        public PurchaseCardException(Player player, Card card)
            : base(player, card)
        {
        }

        #endregion
    }
}