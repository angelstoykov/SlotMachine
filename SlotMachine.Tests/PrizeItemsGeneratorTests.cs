using System;
using SlotMachine.Core;
using SlotMachine.Models.PrizeItems;

namespace SlotMachine.Tests
{
    public class PrizeItemsGeneratorTests
    {
        [Test]
        public void TestPrizeItemsGenerator()
        {
            PrizeItemBase[] actualItems = PrizeGenerator.GeneratePrizeItems();
            var actualApple = actualItems.First(a => a.Name == "Apple");
            var actualBanana = actualItems.First(a => a.Name == "Banana");
            var actualPineapple = actualItems.First(a => a.Name == "Pineapple");
            var actualWildCard = actualItems.First(a => a.Name == "WildCard");

            var expectedApple = new Apple("Apple", "A");
            var expectedBanana = new Banana("Banana", "B");
            var expectedPineapple = new Pineapple("Pineapple", "P");
            var expectedWildCard = new WildCard("WildCard", "*");

            Assert.That(actualApple.Name, Is.EqualTo(expectedApple.Name));
            Assert.That(actualBanana.Name, Is.EqualTo(expectedBanana.Name));
            Assert.That(actualPineapple.Name, Is.EqualTo(expectedPineapple.Name));
            Assert.That(actualWildCard.Name, Is.EqualTo(expectedWildCard.Name));
        }
    }
}