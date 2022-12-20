namespace SlotMachine.Models.PrizeItems
{
    internal abstract class PrizeItemBase
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
