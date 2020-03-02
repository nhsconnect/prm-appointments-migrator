using System;
using System.Linq;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.Slot;
using Newtonsoft.Json;

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
        public async Task<SlotResponse> GetSlots(DateTime start, DateTime end, SourceTarget sourceTarget = SourceTarget.Target)
        {
            var response = await _clientWrapper.GetSlotsHttp(start, end, sourceTarget);
            var slots = _deserializer.Deserialize(response);

            return slots;
        }
    }
}