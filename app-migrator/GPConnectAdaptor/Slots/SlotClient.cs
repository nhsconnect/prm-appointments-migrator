using System;
using System.Linq;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.Slot;

namespace GPConnectAdaptor.Slots
{
    public class SlotClient : Slots.ISlotClient
    {
        private readonly ISlotHttpClientWrapper _clientWrapper;
        private readonly Slots.ISlotResponseDeserializer _deserializer;

        public SlotClient(ISlotHttpClientWrapper clientWrapper, Slots.ISlotResponseDeserializer deserializer)
        {
            _clientWrapper = clientWrapper;
            _deserializer = deserializer;
        } 
        public async Task<SlotResponse> GetSlots(DateTime start, DateTime end)
        {
            var response = await _clientWrapper.GetAsync(start, end);
            var slots = _deserializer.Deserialize(response);

            return slots;

        }
    }
}