namespace SlotMachine.Common.Messages
{
    public static class ExceptionMessages
    {
        public const string CAN_NOT_ADD_ZERO_OR_NEGATIVE_DEPOSIT = "You can not add zero or negative currency value to your wallet.";

        public const string CAN_NOT_BET_NEGATIVE_AMOUNT = "You can not bet negative amount.";

        public const string UNSUFFICIANT_FUNDS = "Unsufficiend funds for that bet in your wallet. Your balance is: {0:F2}";

        public const string PLAYER_NAME_CAN_NOT_BE_EMPTY = "Player's name cannot be empty.";

        public const string UNSUCCESSFUL_DEPOSIT_PARSE = "Please deposit valid amount.";

        public const string UNSUCCESSFUL_BET_PARSE = "Please add valid bet amount.";
    }
}
