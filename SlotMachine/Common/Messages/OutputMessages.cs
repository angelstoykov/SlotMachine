namespace SlotMachine.Common.Messages
{
    internal static class OutputMessages
    {
        public const string PROMPT_TO_DEPOSIT = "Please deposit the amount you would like to play with:";

        public const string PROMPT_TO_BET = "Enter your bet to begin:";

        public const string ZERO_BALANCE_PROMPT_TO_DEPOSIT = "Your balance cannot allow you continue playing.\nConsider deposit new amount and try again.";

        public const string WINNING_LINE = "The winning line is: {0}";

        public const string WINNING_MESSAGE = "Winning coefficient: {0:F2}. You have won {1:F2}. Your balance is {2:F2}";
    }
}
