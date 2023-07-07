namespace SlotMachine.Common.Messages
{
    internal static class ExceptionMessages
    {
        public const string CAN_NOT_ADD_ZERO_OR_NEGATIVE_DEPOSIT = "You can not add zero or negative currency value to your wallet.";

        public const string CAN_NOT_BET_NEGATIVE_AMOUNT = "You can not bet negative amount.";

        public const string UNSUFFICIANT_FUNDS = "Unsufficiend funds for that bet in your wallet. Your balance is: {0:F2}";

        public const string PLAYER_NAME_CAN_NOT_BE_EMPTY = "Player's name cannot be empty.";
    }
}
