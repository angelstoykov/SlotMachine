using SlotMachine.IO.Contracts;

namespace SlotMachine.IO
{
    internal class Writer : IWriter
    {
        public void WriteLine(string message) => Console.WriteLine(message);
    }
}
