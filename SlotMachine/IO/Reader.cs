using SlotMachine.IO.Contracts;

namespace SlotMachine.IO
{
    internal class Reader : IReader
    {
        public string ReadLine() => Console.ReadLine();
    }
}
