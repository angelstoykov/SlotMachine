using SlotMachine.Common.Messages;
using SlotMachine.Models.Players.Contracts;

namespace SlotMachine.Models.Account
{
    public class Player : IPlayer
    {
        private string name;

        public string Name
        {
            get => this.name;

            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.PLAYER_NAME_CAN_NOT_BE_EMPTY);
                }
                this.name = value;
            }
        }

        public Wallet Wallet { get; set; }

        public Player(string name)
        {
            this.Wallet = new Wallet();

            this.Name = name;
        }

        public decimal Bet(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException(ExceptionMessages.CAN_NOT_BET_NEGATIVE_AMOUNT);
            }

            if (this.Wallet.Balance >= amount)
            {
                return this.Wallet.WithdrawBet(amount);
            }
            else
            {
                throw new ArgumentException(string.Format(ExceptionMessages.UNSUFFICIANT_FUNDS, this.Wallet.Balance));
            }
        }

        public void Deposit(decimal amount)
        {
            this.Wallet.Deposit(amount);
        }

        public void DepositFromWinningBet(decimal amount)
        {
            if (amount > 0)
            {
                this.Wallet.Deposit(amount);
            }
        }
    }
}
