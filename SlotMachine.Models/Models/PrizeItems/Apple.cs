using System.Xml.Linq;

namespace SlotMachine.Models.PrizeItems
{
    public class Apple : PrizeItemBase
    {
        public const decimal WINNING_COEFFICIENT = 0.4m;
        public const int PROBABILITY_TO_APPEAR = 45; // 45% should appear in 45%

        public Apple()
            : this(typeof(Apple).Name,
                   typeof(Apple).Name.Substring(0, 1).ToUpper())
        {
        }

        public Apple(string name, string representation)
            : base(name, representation, PROBABILITY_TO_APPEAR, WINNING_COEFFICIENT)
        {
        }
    }
}
