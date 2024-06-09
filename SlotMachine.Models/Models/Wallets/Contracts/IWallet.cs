namespace SlotMachine.Models.Wallets.Contracts
{
    public interface IWallet
    {
        decimal Balance { get; set; }

        decimal Checkout();
        void Deposit(decimal amount);
        decimal WithdrawBet(decimal amount);
    }
}