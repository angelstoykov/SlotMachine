using SlotMachine.Models.PrizeItems;
using SlotMachine.Models.PrizeItems.Contracts;
using System.Reflection;

namespace SlotMachine.Core
{
    public static class PrizeGenerator
    {
        private const string targetNamespace = "SlotMachine.Models.PrizeItems";

        public static List<IPrizeItem> GeneratePrizeItems()
        {
            var assemblyPath = $"{AppDomain.CurrentDomain.BaseDirectory}SlotMachine.Models.dll";
            var prizeItemsTypes = GetPrizeItemTypesInNamespace(targetNamespace, assemblyPath);
            var prizeItems = new List<IPrizeItem>();

            foreach (var type in prizeItemsTypes)
            {
                try
                {
                    // Create an instance using the factory
                    IPrizeItem item = CreateInstance(type);
                    if (item != null && !prizeItems.Contains(item))
                    {
                        prizeItems.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to create instance of {type.Name}: {ex.Message}");
                }
            }

            return prizeItems;
        }

        public static IPrizeItem CreateInstance(Type type)
        {
            if (typeof(IPrizeItem).IsAssignableFrom(type) && !type.IsAbstract)
            {
                // Create an instance of the class using reflection
                return (IPrizeItem)Activator.CreateInstance(type);
            }
            throw new ArgumentException("The type must implement IPrizeItem and not be abstract.");
        }

        public static Type[] GetPrizeItemTypesInNamespace(string targetNamespace, string assemblyPath)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            

            return assembly.GetTypes()
                           .Where(t => typeof(IPrizeItem).IsAssignableFrom(t)
                                       && t.Namespace == targetNamespace
                                       && !t.IsAbstract)
                           .ToArray();
        }
    }
}
