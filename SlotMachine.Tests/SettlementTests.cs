using SlotMachine.Core;
using SlotMachine.Models.PrizeItems;

namespace SlotMachine.Tests
{
    internal class SettlementTests
    {
        [Test]
        public void CalculateProfitTest()
        {
            const decimal bet = 100m;
            const decimal expectedWinningCoefficientSameLetters = 5.4m;
            const decimal expectedWinningCoefficientWithAsterix = 4.6m;

            var actualWinningLinesWithSameLetters = GetWinningLinesSameLetters();
            var actualWinningLinesWithAsterix = GetWinningLinesWithAsterix();

            PrizeItemBase[] prizeItems = PrizeGenerator.GeneratePrizeItems();

            var profitSameLetters = Settlement.CalculateProfit(bet, actualWinningLinesWithSameLetters, prizeItems);
            var profitWithAsterix = Settlement.CalculateProfit(bet, actualWinningLinesWithAsterix, prizeItems);

            Assert.That(Math.Round((bet * expectedWinningCoefficientSameLetters), 2), Is.EqualTo(profitSameLetters));
            Assert.That(Math.Round((bet * expectedWinningCoefficientWithAsterix), 2), Is.EqualTo(profitWithAsterix));
        }

        [Test]
        public void CalculateWinningCoefficientTest()
        {
            const decimal expectedWinningCoefficientSameLetters = 5.4m;
            const decimal expectedWinningCoefficientWithAsterix = 4.6m;
            PrizeItemBase[] prizeItems = PrizeGenerator.GeneratePrizeItems();

            var actualWinningLines = GetWinningLinesSameLetters();
            var actualWinningCoefficient = Settlement.CalculateProfitCoeficient(actualWinningLines, prizeItems);

            var actualWinningLinesWithAsterix = GetWinningLinesWithAsterix();
            var actualWinningCoefficientWithAsterix = Settlement.CalculateProfitCoeficient(actualWinningLinesWithAsterix, prizeItems);

            Assert.That(actualWinningCoefficient, Is.EqualTo(expectedWinningCoefficientSameLetters));
            Assert.That(actualWinningCoefficientWithAsterix, Is.EqualTo(expectedWinningCoefficientWithAsterix));
        }

        [Test]
        public void SettlementEvaluateResultTest()
        {
            var actualLinesWithSameLetters = GetWinningLinesSameLetters();
            var actualWinningLinesWithAsterix = GetWinningLinesWithAsterix();
            var actualNoWinningLines = GetNoWinningLines();

            var expectedLinesWithSameLetters = Settlement.EvaluateResult(actualLinesWithSameLetters);
            var expectedLinesWithWithAsterix = Settlement.EvaluateResult(actualWinningLinesWithAsterix);
            var expectedNoWinningLinesCount = Settlement.EvaluateResult(actualNoWinningLines).Count;

            CollectionAssert.AreEqual(expectedLinesWithSameLetters, actualLinesWithSameLetters);
            Assert.That(expectedLinesWithSameLetters.Count, Is.EqualTo(actualLinesWithSameLetters.Count));

            CollectionAssert.AreEqual(expectedLinesWithWithAsterix, actualWinningLinesWithAsterix);
            Assert.That(expectedLinesWithWithAsterix.Count, Is.EqualTo(actualWinningLinesWithAsterix.Count));

            // There is no winning line
            Assert.That(expectedNoWinningLinesCount, Is.EqualTo(0));
        }

        private List<string> GetWinningLinesSameLetters()
        {
            return new List<string>
            {
                "AAA",
                "BBB",
                "PPP",
                "***"
            };
        }

        private List<string> GetWinningLinesWithAsterix()
        {
            return new List<string>
            {
                "AA*",
                "B*B",
                "*PP",
                "**A",
                "B**"
            };
        }

        private List<string> GetNoWinningLines()
        {
            return new List<string>
            {
                "ABP",
                "B*A",
                "*PB",
                "fndhsjak"
            };
        }
    }
}
