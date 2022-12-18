using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Common.Messages
{
    internal class ExceptionMessages
    {
        public const string CAN_NOT_ADD_ZERO_DEPOSIT = "You can not add zero currency to your wallet.";

        public const string CAN_NOT_BET_NEGATIVE_AMOUNT = "You can not bet negative amount.";

        public const string UNSUFFICIANT_FUNDS = "Unsufficiend funds for that bet in your wallet. Your balance is: {0:F2}";
    }
}
