using SlotMachine.Models.PrizeItems.Contracts;

namespace SlotMachine.Services.Contracts
{
    public interface ISettlement
    {
        List<string> EvaluateResult(List<string> slotSpine);
        
        decimal CalculateProfitCoeficient(List<string> winningLines, List<IPrizeItem> prizeItems);
        
        decimal CalculateProfit(decimal bet, List<string> winningLines, List<IPrizeItem> prizeItems);
    }
}
