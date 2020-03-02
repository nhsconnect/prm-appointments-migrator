using System;
using GPConnectAdaptor.Models.AddAppointment;

namespace GPConnectAdaptor.AddAppointment
{
    public class AddAppointmentResponseDeserializer : IAddAppointmentResponseDeserializer
    {
        public AddAppointmentResponse Deserialize(string response)
        {
            try
            {
                return System.Text.Json.JsonSerializer.Deserialize<AddAppointmentResponse>(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"unable to deserialise appointment response. RESPONSE: '{response}'");
            }
        }
    }
}