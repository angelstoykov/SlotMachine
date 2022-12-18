using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Models.PrizeItems
{
    internal class Pineapple : PrizeItemBase
    {
        private const decimal WINNING_COEFFICIENT = 0.8m;
        private const int PROBABILITY_TO_APPEAR = 15; // should appear in 15%

        public Pineapple(string name, string representation)
            : base (name, representation, PROBABILITY_TO_APPEAR, WINNING_COEFFICIENT)
        {
        }
    }
}
