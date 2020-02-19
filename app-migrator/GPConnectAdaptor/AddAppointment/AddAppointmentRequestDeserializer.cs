using GPConnectAdaptor.Models.AddAppointment;
using Newtonsoft.Json;

namespace GPConnectAdaptor.AddAppointment
{
    public class AddAppointmentRequestDeserializer : IAddAppointmentRequestDeserializer
    {
        public AddAppointmentRequest Deserialize(string request)
        {
            return JsonConvert.DeserializeObject<AddAppointmentRequest>(request);
        }
    }
}