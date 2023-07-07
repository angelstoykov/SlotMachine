using SlotMachine.Models.PrizeItems;

namespace SlotMachine.Core.Contracts
{
    public interface ISpinGenerator
    {
        List<string> CreateSlotSpin(PrizeItemBase[] prizeItems);
    }
}