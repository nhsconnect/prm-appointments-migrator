using System;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.Slot;

namespace GPConnectAdaptor.Slots
{
    public interface ISlotClient
    {
        Task<SlotResponse> GetSlots(DateTime start, DateTime end);
    }
}