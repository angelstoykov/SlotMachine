using SlotMachine.Models.Wallets.Contracts;

namespace SlotMachine.Models.Players.Contracts
{
    internal interface IPlayer
    {
        string Name { get; set; }

        IWallet Wallet { get; set; }

        decimal Bet(decimal amount);

        void Deposit(decimal amount);

        void DepositFromWinningBet(decimal amount);
    }
}
