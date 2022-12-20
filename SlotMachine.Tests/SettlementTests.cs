using SlotMachine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Tests
{
    internal class SettlementTests
    {
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
