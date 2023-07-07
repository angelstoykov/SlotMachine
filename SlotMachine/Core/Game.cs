using SlotMachine.Common.Messages;
using SlotMachine.Core.Contracts;
using SlotMachine.IO.Contracts;
using SlotMachine.Models.Players.Contracts;
using SlotMachine.Models.PrizeItems;

namespace SlotMachine.Core
{
    internal class Game : IGame
    {
        private IReader reader;
        private IWriter writer;
        private PrizeItemBase[] prizeItems;
        private ISpinGenerator spinGenerator;
        private IPlayer player;

        public Game(IReader reader, IWriter writer, ISpinGenerator spinGenerator, IPlayer player)
        {
            this.reader = reader;
            this.writer = writer;
            this.prizeItems = PrizeGenerator.GeneratePrizeItems();
            this.spinGenerator = spinGenerator;
            this.player = player;
        }

        public PrizeItemBase[] PrizeItems { get => this.prizeItems; }

        public void Play()
        {
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
                writer.WriteLine(string.Format(OutputMessages.PROMPT_TO_BET, player.Wallet.Balance));

                var isValidAmount = decimal.TryParse(reader.ReadLine(), out var bet);

                if (isValidAmount && player.Wallet.Balance > 0)
                {
                    try
                    {
                        // Bet
                        var currentBalance = player.Bet(bet); ;

                        // Create result
                        var slotSpine = spinGenerator.CreateSlotSpin(PrizeItems);

                        // Visualize result
                        foreach (var line in slotSpine)
                        {
                            writer.WriteLine(line);
                        }

                        // Evauate result
                        var winningLines = Settlement.EvaluateResult(slotSpine);

                        if (winningLines.Count > 0)
                        {
                            var profitCoefficient = Settlement.CalculateProfitCoeficient(winningLines, PrizeItems);
                            var profit = Settlement.CalculateProfit(bet, winningLines, PrizeItems);

                            player.DepositFromWinningBet(profit);
                            writer.WriteLine(string.Format(OutputMessages.WINNING_MESSAGE, profitCoefficient, profit));
                        }
                        else
                        {
                            writer.WriteLine(OutputMessages.NO_WINNING_LINE_MESSAGE);
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
    }
}
