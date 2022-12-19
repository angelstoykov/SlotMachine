using SlotMachine.Models.PrizeItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Core
{
    internal static class PrizeGenerator
    {
        public static PrizeItemBase[] GeneratePrizeItems()
        {
            PrizeItemBase[] obj =  {
                new Apple("Apple", "A"),
                new Banana("Banana", "B"),
                new Pineapple("Pineapple", "P"),
                new WildCard("WildCard", "*")
            };
            return obj;
        }
    }
}
