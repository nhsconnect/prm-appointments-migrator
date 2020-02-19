using GPConnectAdaptor.Models.Slot;

namespace GPConnectAdaptor.Slots
{
    public interface ISlotResponseDeserializer
    {
        public SlotResponse Deserialize(string response);
    }
}