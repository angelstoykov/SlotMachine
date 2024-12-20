﻿using SlotMachine.Models.PrizeItems.Contracts;

namespace SlotMachine.Core.Contracts
{
    public interface ISpinGenerator
    {
        List<string> CreateSlotSpin(IList<IPrizeItem> prizeItems);
    }
}