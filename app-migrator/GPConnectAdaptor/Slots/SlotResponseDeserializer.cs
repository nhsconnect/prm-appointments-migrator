using GPConnectAdaptor.Models;
using GPConnectAdaptor.Models.Slot;
using Newtonsoft.Json;

namespace GPConnectAdaptor.Slots
{
    public class SlotResponseDeserializer : Slots.ISlotResponseDeserializer
    {
        public SlotResponse Deserialize(string response)
        {
            var deserializedResponse = JsonConvert.DeserializeObject<SlotResponse>(response);
            return deserializedResponse;
        }
    }
}