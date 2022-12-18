using SlotMachine.IO.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.IO
{
    internal class Writer : IWriter
    {
        public void WriteLine(string message) => Console.WriteLine(message);
    }
}
