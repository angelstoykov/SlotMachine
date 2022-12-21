using SlotMachine.Models.PrizeItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Core
{
    internal class SpinGenerator
    {
        public List<string> CreateSlotSpin(PrizeItemBase[] prizeItems)
        {
            var slotSpin = new List<string>();

            for (var i = 0; i < 4; i++)
            {
                var slotLine = CreateSlotLine(prizeItems);
                slotSpin.Add(slotLine);
            }

            return slotSpin;
        }

        private string CreateSlotLine(PrizeItemBase[] prizeItems)
        {
            var slotLine = string.Empty;

            while (slotLine.Length < 3)
            {
                var index = GenerateRandomNumberInRange(0, prizeItems.Length);
                var randomNumber = GenerateRandomNumberInRange(1, 101);

                var prizeItemRepresentation = prizeItems[index].Representation;
                var prizeItemProbabilityToAppear = prizeItems[index].ProbabilityToAppear;

                if (prizeItemRepresentation == "A" && prizeItemProbabilityToAppear >= randomNumber)
                {
                    slotLine += prizeItemRepresentation;
                }
                else if (prizeItemRepresentation == "B" && prizeItemProbabilityToAppear >= randomNumber)
                {
                    slotLine += prizeItemRepresentation;
                }
                else if (prizeItemRepresentation == "P" && prizeItemProbabilityToAppear >= randomNumber)
                {
                    slotLine += prizeItemRepresentation;
                }
                else if (prizeItemRepresentation == "*" && prizeItemProbabilityToAppear >= randomNumber)
                {
                    slotLine += prizeItemRepresentation;
                }
            }

            return slotLine;
        }

        private int GenerateRandomNumberInRange(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }
    }
}
