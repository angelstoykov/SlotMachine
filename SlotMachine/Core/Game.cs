using SlotMachine.Common.Messages;
using SlotMachine.Core.Contracts;
using SlotMachine.IO;
using SlotMachine.IO.Contracts;
using SlotMachine.Models.Account;
using SlotMachine.Models.PrizeItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Core
{
    internal class Game : IGame
    {
        private PrizeItemBase[] prizeItems =
            {
                new Apple("Apple", "A"),
                new Banana("Banana", "B"),
                new Pineapple("Pineapple", "P"),
                new WildCard("WildCard", "*")
            };

        private IWriter writer;
        private IReader reader;

        public Game()
        {
            this.writer = new Writer();
            this.reader = new Reader();
        }

        public void Play()
        {
            var player = new Player("Angel");

            writer.WriteLine(OutputMessages.PROMPT_TO_DEPOSIT);

            var success = decimal.TryParse(reader.ReadLine(), out var amountToDeposit);

            if (success)
            {
                try
                {
                    player.Deposit(amountToDeposit);
                }
                catch (ArgumentException e)
                {
                    writer.WriteLine(e.Message);
                    return;
                }
            }

            while (player.Wallet.Balance > 0m)
            {
                writer.WriteLine(OutputMessages.PROMPT_TO_BET);
                var isValidAmount = decimal.TryParse(reader.ReadLine(), out var bet);

                if (isValidAmount && player.Wallet.Balance > 0 && bet > 0)
                {
                    try
                    {
                        // Bet
                        var currentBalance = player.Bet(bet);

                        // Create result
                        var slotSpine = CreateSlotSpin();

                        // Visualize result
                        foreach (var line in slotSpine)
                        {
                            writer.WriteLine(line);
                        }

                        // Evauate result
                        EvaluateResult(slotSpine);
                    }
                    catch (Exception e)
                    {
                        writer.WriteLine(e.Message);
                    }
                }
            }

            writer.WriteLine(OutputMessages.ZERO_BALANCE_PROMPT_TO_DEPOSIT);
        }

        private void EvaluateResult(List<string> slotSpine)
        {
            var coef1 = 0m;
            var coef2 = 0m;

            foreach (var line in slotSpine)
            {
                var areAllCharsSame = AreAllCharsSame(line);

                if (areAllCharsSame)
                {
                    writer.WriteLine(string.Format(OutputMessages.ALL_CHARS_ARE_SAME, line));
                    // Calculate profit for all chars are same
                    coef1 += Settlement.CalculateWinningLineCoefficient(line, prizeItems);
                }
                else if (IsStringContainsAsterix(line))
                {
                    var lineWithoutAsterix = line.Replace("*", string.Empty);
                    var areTheRestCharsSame = AreAllCharsSame(lineWithoutAsterix);

                    if (areTheRestCharsSame)
                    {
                        writer.WriteLine(string.Format(OutputMessages.LINE_WITH_ASTERIX_AND_SAME_LETTERS, line));
                        coef2 += Settlement.CalculateWinningLineCoefficient(line, prizeItems);
                    }
                }
            }
            writer.WriteLine(coef1+ " " + coef2);
        }

        private bool IsStringContainsAsterix(string line)
        {
            return line.Contains("*");
        }

        private bool AreAllCharsSame(string line)
        {
            return line.All(c => c == line[0]);
        }

        private List<string> CreateSlotSpin()
        {
            var slotSpin = new List<string>();

            for (var i = 0; i < 4; i++)
            {
                var slotLine = CreateSlotLine();
                slotSpin.Add(slotLine);
            }

            return slotSpin;
        }

        private string CreateSlotLine()
        {
            var slotLine = string.Empty;

            while (slotLine.Length < 3)
            {
                var index = GenerateIndex();
                var randomNumber = GenerateRandomNumberInRange(1, 101);

                var prizeItemRepresentation = prizeItems[index].Representation;
                var prizeItemProbabilityToAppear = prizeItems[index].ProbabilityToAppear;

                if (prizeItemRepresentation == "A" && prizeItemProbabilityToAppear <= randomNumber)
                {
                    slotLine += prizeItemRepresentation;
                } 
                else if (prizeItemRepresentation == "B" && prizeItemProbabilityToAppear <= randomNumber)
                {
                    slotLine += prizeItemRepresentation;
                }
                else if (prizeItemRepresentation == "P" && prizeItemProbabilityToAppear <= randomNumber)
                {
                    slotLine += prizeItemRepresentation;
                }
                else if (prizeItemRepresentation == "*" && prizeItemProbabilityToAppear <= randomNumber)
                {
                    slotLine += prizeItemRepresentation;
                }
            }

            return slotLine;
        }

        private int GenerateIndex()
        {
            var random = new Random();

            return random.Next(0, prizeItems.Length);
        }

        private int GenerateRandomNumberInRange(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }
    }
}
