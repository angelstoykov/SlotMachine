using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Common.Messages
{
    internal static class OutputMessages
    {
        public const string PROMPT_TO_DEPOSIT = "Please deposit the amount you would like to play with:";

        public const string PROMPT_TO_BET = "Enter your bet to begin:";

        public const string ZERO_BALANCE_PROMPT_TO_DEPOSIT = "Your balance cannot allow you continue playing.\nConsider deposit new amount and try again.";

        public const string ALL_CHARS_ARE_SAME = "Line with same chars {0}";

        public const string LINE_WITH_ASTERIX_AND_SAME_LETTERS = "Line with contains ASTERIX/TWO asterix and one/two same letters: {0}";
    }
}
