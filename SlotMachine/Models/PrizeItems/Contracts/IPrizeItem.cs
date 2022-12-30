using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Models.PrizeItems.Contracts
{
    internal interface IPrizeItem
    {
        string Name { get; }

        string Representation { get; }

        int ProbabilityToAppear { get; }

        decimal WinningCoefficient { get; }
    }
}
