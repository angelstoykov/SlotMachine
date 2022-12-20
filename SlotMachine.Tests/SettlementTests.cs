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
            var actualLines = new List<string> { "AA*" };

            var expectedLines = Settlement.EvaluateResult(actualLines);

            CollectionAssert.AreEqual(expectedLines, actualLines);
        }
    }
}
