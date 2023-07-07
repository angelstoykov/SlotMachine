using SlotMachine.Common.Messages;
using SlotMachine.Models.Wallets.Contracts;

namespace SlotMachine.Models.Wallets
{
    public class Wallet : IWallet
    {
        private decimal balance;

        public decimal Balance
        {
            get { return balance; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("You cannot add negative values to the wallet's balance");
                }
                balance = value;
            }
        }

        public Wallet()
        {
            Balance = 0;
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.CAN_NOT_ADD_ZERO_OR_NEGATIVE_DEPOSIT);
            }
        }

        public decimal WithdrawBet(decimal amount)
        {
            Balance -= amount;
            return Balance;
        }

        public decimal Checkout()
        {
            return Balance;
        }

    }
}
