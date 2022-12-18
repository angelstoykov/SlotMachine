using SlotMachine.Core.Contracts;
using SlotMachine.Models.PrizeItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Core
{
    internal static class Settlement
    {
        public static decimal CalculateWinningLineCoefficient(string line, PrizeItemBase[] prizeItems)
        {
            var totalProfitCoefficient = 0m;

            foreach (var ch in line)
            {
                switch (ch)
                {
                    case 'A':
                        totalProfitCoefficient += prizeItems.First(pi => pi.Representation == "A").WinningCoefficient; ;
                        break;
                    case 'B':
                        totalProfitCoefficient += prizeItems.First(pi => pi.Representation == "B").WinningCoefficient; ;
                        break;
                    case 'P':
                        totalProfitCoefficient += prizeItems.First(pi => pi.Representation == "P").WinningCoefficient; ;
                        break;
                    case '*':
                        totalProfitCoefficient += prizeItems.First(pi => pi.Representation == "*").WinningCoefficient; ;
                        break;
                }
            }

            return totalProfitCoefficient;
        }
    }
}
