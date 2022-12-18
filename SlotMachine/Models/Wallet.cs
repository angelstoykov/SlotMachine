using SlotMachine.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Models
{
    public class Wallet
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
				this.balance = value;
			}
		}

		public Wallet()
		{
			this.Balance = 0;
		}

		public void Deposit(decimal amount)
		{
			if (amount > 0)
			{
				this.Balance += amount;
			}
			else
			{
				throw new ArgumentException(ExceptionMessages.CAN_NOT_ADD_ZERO_DEPOSIT);
			}
		}

		public decimal WithdrawBet(decimal amount)
		{
            this.Balance -= amount;
			return this.Balance;
		}

		public decimal Checkout()
		{
			return this.Balance;
		}

    }
}
