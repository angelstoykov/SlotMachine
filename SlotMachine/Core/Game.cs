﻿using SlotMachine.Common.Messages;
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
        private IReader reader;
        private IWriter writer;
        private PrizeItemBase[] prizeItems;

        public Game()
        {
            this.reader = new Reader();
            this.writer = new Writer();
            this.prizeItems = PrizeGenerator.GeneratePrizeItems();
        }

        public void Play()
        {
            var player = new Player("Angel");

            while (player.Wallet.Balance == 0m)
            {
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
                    }
                }
            }

            while (player.Wallet.Balance > 0m)
            {
                writer.WriteLine(OutputMessages.PROMPT_TO_BET);
                var isValidAmount = decimal.TryParse(reader.ReadLine(), out var bet);

                if (isValidAmount && player.Wallet.Balance > 0)
                {
                    try
                    {
                        // Bet
                        var currentBalance = player.Bet(bet); ;

                        // Create result
                        var slotSpine = CreateSlotSpin();

                        // Visualize result
                        foreach (var line in slotSpine)
                        {
                            writer.WriteLine(line);
                        }

                        // Evauate result
                        var profitCoefficient = EvaluateResult(slotSpine);

                        if (profitCoefficient > 0m)
                        {
                            var winningBet = Settlement.CalculateProfit(bet, profitCoefficient);
                            player.DepositFromWinningBet(winningBet);
                            writer.WriteLine(string.Format(OutputMessages.WINNING_MESSAGE, profitCoefficient, winningBet, player.Wallet.Balance));
                        }
                    }
                    catch (Exception e)
                    {
                        writer.WriteLine(e.Message);
                    }
                }
            }

            writer.WriteLine(OutputMessages.ZERO_BALANCE_PROMPT_TO_DEPOSIT);
        }

        private decimal EvaluateResult(List<string> slotSpine)
        {
            var coefficient = 0m;

            foreach (var line in slotSpine)
            {
                if (AreAllCharsSame(line) || IsStringContainsAsterix(line))
                {
                    writer.WriteLine(string.Format(OutputMessages.WINNING_LINE, line));
                    coefficient += Settlement.CalculateWinningLineCoefficient(line, prizeItems);
                }
            }

            return coefficient;
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
