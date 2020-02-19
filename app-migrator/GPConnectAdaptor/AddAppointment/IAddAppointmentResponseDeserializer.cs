using GPConnectAdaptor.Models.AddAppointment;

namespace GPConnectAdaptor
{
    public interface IAddAppointmentResponseDeserializer
    {
        AddAppointmentResponse Deserialize(string response);
    }
}