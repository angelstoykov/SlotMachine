using SlotMachine.Models.PrizeItems.Contracts;

namespace SlotMachine.Core.Contracts
{
    public interface IPrizeGenerator
    {
        IList<IPrizeItem> GeneratePrizeItems();
    }
}