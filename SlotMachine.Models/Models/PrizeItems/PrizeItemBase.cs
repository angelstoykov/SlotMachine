using SlotMachine.Models.PrizeItems.Contracts;

namespace SlotMachine.Models.PrizeItems
{
    public abstract class PrizeItemBase : IPrizeItem
    {
        public PrizeItemBase(string name, string representation, int probabilityToAppear, decimal winningCoefficient)
        {
            Name = name;
            Representation = representation;
            ProbabilityToAppear = probabilityToAppear;
            WinningCoefficient = winningCoefficient;
        }

        public string Name { get; }

        public string Representation { get; }

        public int ProbabilityToAppear { get; }

        public decimal WinningCoefficient { get; }
    }
}
