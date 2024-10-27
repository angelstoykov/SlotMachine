using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Tests.Extensions
{
    internal static class FixtureExtensions
    {
        public static decimal CreateDecimalInRange(this IFixture fixture, decimal min, decimal max)
        {
            return fixture.Create<decimal>() % (max - min + 1) + min;
        }
    }
}
