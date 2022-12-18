using SlotMachine.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Models.Account
{
    public class Player
    {
        public string Name { get; set; }

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
                throw new Exception(ExceptionMessages.CAN_NOT_BET_NEGATIVE_AMOUNT);
            }

            if (this.Wallet.Balance >= amount)
            {
                return this.Wallet.WithdrawBet(amount);
            }
            else
            {
                throw new Exception(string.Format(ExceptionMessages.UNSUFFICIANT_FUNDS, this.Wallet.Balance));
            }
        }

        public void Deposit(decimal amount)
        {
            this.Wallet.Deposit(amount);
        }

        public void DepositFromWinningBet(decimal amount)
        {
            this.Wallet.Deposit(amount);
        }
    }
}
