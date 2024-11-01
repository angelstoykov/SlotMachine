using SlotMachine.Models.PrizeItems;
using SlotMachine.Models.PrizeItems.Contracts;
using SlotMachine.Services.Contracts;

namespace SlotMachine.Services.Settlement
{
    public class Settlement : ISettlement
    {
        private decimal CalculateWinningLineCoefficient(string line, IList<IPrizeItem> prizeItems)
        {
            var totalProfitCoefficient = 0m;

            foreach (var ch in line)
            {
                var r = prizeItems.Where(pi => pi.Representation == ch.ToString()).FirstOrDefault();

                if (r != null)
                {
                    totalProfitCoefficient += r.WinningCoefficient;
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
            return AreAllCharsSame(line) || IsLineWinningWithWildCard(line);
        }

        private bool IsLineWinningWithWildCard(string line)
        {
            if (IsLineContainsAsterix(line))
            {
                var lineWithoutAsterix = line.Replace("*", "");
                return AreAllCharsSame(lineWithoutAsterix);
            }
            return false;
        }

        private bool IsLineContainsAsterix(string line)
        {
            return line.Contains("*");
        }

        private bool AreAllCharsSame(string line)
        {
            return line.All(c => c == line[0]);
        }

        public decimal CalculateProfit(decimal bet, List<string> winningLines, IList<IPrizeItem> prizeItems, decimal profitCoefficient)
        {
            return Math.Round(bet * profitCoefficient, 2);
        }

        public decimal CalculateProfitCoeficient(List<string> winningLines, IList<IPrizeItem> prizeItems)
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
