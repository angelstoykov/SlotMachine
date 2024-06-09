using SlotMachine.Models.PrizeItems;

namespace SlotMachine.Services.Contracts
{
    public interface ISettlement
    {
        List<string> EvaluateResult(List<string> slotSpine);
        
        decimal CalculateProfitCoeficient(List<string> winningLines, PrizeItemBase[] prizeItems);
        
        decimal CalculateProfit(decimal bet, List<string> winningLines, PrizeItemBase[] prizeItems);
    }
}
