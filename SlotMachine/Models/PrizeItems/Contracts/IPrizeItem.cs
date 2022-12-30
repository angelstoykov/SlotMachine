namespace SlotMachine.Models.PrizeItems.Contracts
{
    internal interface IPrizeItem
    {
        string Name { get; }

        string Representation { get; }

        int ProbabilityToAppear { get; }

        decimal WinningCoefficient { get; }
    }
}
