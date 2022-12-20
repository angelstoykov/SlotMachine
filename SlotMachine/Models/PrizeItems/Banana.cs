using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Models.PrizeItems
{
    public class Banana : PrizeItemBase
    {
        private const decimal WINNING_COEFFICIENT = 0.6m;
        private const int PROBABILITY_TO_APPEAR = 35; // should appear in 35%

        public Banana(string name, string representation)
            : base (name, representation, PROBABILITY_TO_APPEAR, WINNING_COEFFICIENT)
        {
        }
    }
}
