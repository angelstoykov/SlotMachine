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
            if (this.Wallet.Balance >= amount && amount > 0)
            {
                return this.Wallet.WithdrawBet(amount);
            }

            throw new Exception($"Unsufficiend funds for that bet in your wallet. Your balance is: {this.Wallet.Balance}");
        }

        public void Deposit(decimal amount)
        {
            this.Wallet.Deposit(amount);
        }
    }
}
