using SlotMachine.IO;
using SlotMachine.IO.Contracts;
using SlotMachine.Models.PrizeItems;

namespace SlotMachine.Core
{
    internal class Settlement
    {
        public decimal CalculateWinningLineCoefficient(string line, PrizeItemBase[] prizeItems)
        {
            var totalProfitCoefficient = 0m;

            foreach (var ch in line)
            {
                switch (ch)
                {
                    case 'A':
                        totalProfitCoefficient += prizeItems.First(pi => pi.Representation == "A").WinningCoefficient;
                        break;
                    case 'B':
                        totalProfitCoefficient += prizeItems.First(pi => pi.Representation == "B").WinningCoefficient;
                        break;
                    case 'P':
                        totalProfitCoefficient += prizeItems.First(pi => pi.Representation == "P").WinningCoefficient;
                        break;
                    case '*':
                        totalProfitCoefficient += prizeItems.First(pi => pi.Representation == "*").WinningCoefficient;
                        break;
                }
            }

            return totalProfitCoefficient;
        }

        public List<string> EvaluateResult(List<string> slotSpine)
        {
            var winningLines = new List<string>();

            foreach (var line in slotSpine)
            {
                if (HasWinningLine(line))
                {
                    winningLines.Add(line);
                }
            }

            return winningLines;
        }

        private bool HasWinningLine(string line)
        {
            return AreAllCharsSame(line) || IsLineContainsAsterix(line);
        }

        private bool IsLineContainsAsterix(string line)
        {
            return line.Contains("*");
        }

        private bool AreAllCharsSame(string line)
        {
            return line.All(c => c == line[0]);
        }

        public decimal CalculateProfit(decimal bet, List<string> winningLines, PrizeItemBase[] prizeItems)
        {
            var profitCoefficient = CalculateProfitCoeficient(winningLines, prizeItems);

            return Math.Round(bet * profitCoefficient, 2);
        }

        public decimal CalculateProfitCoeficient(List<string> winningLines, PrizeItemBase[] prizeItems)
        {
            var profitCoefficient = 0m;

            foreach (var line in winningLines)
            {
                profitCoefficient += CalculateWinningLineCoefficient(line, prizeItems);
            }

            return profitCoefficient;
        }
    }
}
