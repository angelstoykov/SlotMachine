﻿namespace SlotMachine.Models.PrizeItems
{
    public class WildCard : PrizeItemBase
    {
        private const decimal WINNING_COEFFICIENT = 0m;
        private const int PROBABILITY_TO_APPEAR = 5; // should appear in 5%

        public WildCard()
    :       this(typeof(WildCard).Name,
                 "*")
        {
        }

        public WildCard(string name, string representation)
            : base(name, representation, PROBABILITY_TO_APPEAR, WINNING_COEFFICIENT)
        {
        }
    }
}
