using System;
using System.Collections.Generic;
using System.Linq;
using GPConnectAdaptor.Models.AddAppointment;
using GPConnectAdaptor.Patient;
using GPConnectAdaptor.Practitioner;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GPConnectAdaptor.AddAppointment
{
    public class AppointmentBookedModelMapper : IAppointmentBookedModelMapper
    {
        public AppointmentBookedModel Map(string response, IPatientLookup patientLookup, IPractitionerLookup practitionerLookup)
        {
            try
            {
                var parsed = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response);

                JArray participants = parsed["participant"];

                var patientId = GetId(GetPatientResource(participants));
                var practitionerId = GetId(GetPractitionerResource(participants));
                return new AppointmentBookedModel()
                {
                    PatientId = patientId,
                    Patient = patientLookup.GetPatientById(patientId).Name,
                    Location = null,
                    LocationId = GetId(GetLocationResource(participants)),
                    Practitioner = practitionerLookup.GetPractitionerByLocalId(practitionerId.ToString()).Name,
                    PractitionerId = practitionerId,
                    Start = DateTime.Parse(parsed["start"].ToString()),
                    End = DateTime.Parse(parsed["end"].ToString()),
                    Description = parsed["description"].ToString(),
                    Success = true,
                    Error = null
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new AppointmentBookedModel()
                {
                    Success = false,
                    Error = $"Failed to Map book appointment response. Returned with {e.Message}. Response : {response}"
                };
            }
        }
        
        private static int GetId(JToken participant)
        {
            return Int32.Parse(participant["actor"]["reference"].ToString().Split("/")[1]);
        }

        private static JToken GetPatientResource(JToken participants)
        {
            return participants.First(p => p["actor"]["reference"].ToString().Contains("Patient"));
        }
        
        private static JToken GetPractitionerResource(JToken participants)
        {
            return participants.First(p => p["actor"]["reference"].ToString().Contains("Practitioner"));
        }
        
        private static JToken GetLocationResource(JToken participant)
        {
            return participant.First(p => p["actor"]["reference"].ToString().Contains("Location"));
        }
    }
}