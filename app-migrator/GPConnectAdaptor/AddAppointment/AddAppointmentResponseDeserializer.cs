using GPConnectAdaptor.Models.AddAppointment;

namespace GPConnectAdaptor.AddAppointment
{
    public class AddAppointmentResponseDeserializer : IAddAppointmentResponseDeserializer
    {
        public AddAppointmentResponse Deserialize(string response)
        {
            return System.Text.Json.JsonSerializer.Deserialize<AddAppointmentResponse>(response);
        }
    }
}