namespace SlotMachine.Models.PrizeItems.Contracts
{
    public interface IPrizeItem
    {
        string Name { get; }

        string Representation { get; }

        int ProbabilityToAppear { get; }

        decimal WinningCoefficient { get; }
    }
}
