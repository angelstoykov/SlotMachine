using SlotMachine.Common.Messages;
using SlotMachine.Core.Contracts;
using SlotMachine.IO.Contracts;
using SlotMachine.Models.Players.Contracts;
using SlotMachine.Models.PrizeItems;
using SlotMachine.Models.PrizeItems.Contracts;
using SlotMachine.Services.Contracts;

namespace SlotMachine.Core
{
    internal class Game : IGame
    {
        private IReader reader;
        private IWriter writer;
        private List<IPrizeItem> prizeItems;
        private ISpinGenerator spinGenerator;
        private IPlayer player;
        private ISettlement settlement;

        private const decimal ZERO_BALANCE = 0m;

        public Game(IReader reader,
                    IWriter writer,
                    ISpinGenerator spinGenerator,
                    IPlayer player,
                    ISettlement settlement)
        {
            this.reader = reader;
            this.writer = writer;
            this.prizeItems = PrizeGenerator.GeneratePrizeItems();
            this.spinGenerator = spinGenerator;
            this.player = player;
            this.settlement = settlement;
        }

        public List<IPrizeItem> PrizeItems { get => this.prizeItems; }

        public void Play()
        {
            while (player.Wallet.Balance == ZERO_BALANCE)
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
                else
                {
                    writer.WriteLine(ExceptionMessages.UNSUCCESSFUL_DEPOSIT_PARSE);
                }
            }

            while (player.Wallet.Balance > ZERO_BALANCE)
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
                        var winningLines = settlement.EvaluateResult(slotSpine);

                        if (winningLines.Count > 0)
                        {
                            var profitCoefficient = settlement.CalculateProfitCoeficient(winningLines, PrizeItems);
                            var profit = settlement.CalculateProfit(bet, winningLines, PrizeItems);

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
                else
                {
                    writer.WriteLine(ExceptionMessages.UNSUCCESSFUL_BET_PARSE);
                }
            }

            writer.WriteLine(OutputMessages.ZERO_BALANCE_PROMPT_TO_DEPOSIT);
        }
    }
}
