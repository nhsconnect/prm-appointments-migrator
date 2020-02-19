using GPConnectAdaptor.Models.AddAppointment;

namespace GPConnectAdaptor
{
    public interface IAddAppointmentRequestDeserializer
    {
        public AddAppointmentRequest Deserialize(string request);
    }
}