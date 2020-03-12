using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.Slot;

namespace GPConnectAdaptor.Slots
{
    public interface ISlotClient
    {
        Task<List<SlotModel>> GetSlots(DateTime start, DateTime end, SourceTarget sourceTarget = SourceTarget.Target);
    }
}