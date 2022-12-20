using SlotMachine.Models.PrizeItems;

namespace SlotMachine.Core
{
    public static class PrizeGenerator
    {
        public static PrizeItemBase[] GeneratePrizeItems()
        {
            PrizeItemBase[] obj =  {
                new Apple("Apple", "A"),
                new Banana("Banana", "B"),
                new Pineapple("Pineapple", "P"),
                new WildCard("WildCard", "*")
            };
            return obj;
        }
    }
}
