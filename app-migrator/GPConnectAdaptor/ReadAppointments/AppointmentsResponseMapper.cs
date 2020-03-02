using System;
using System.Collections.Generic;
using System.Linq;
using GPConnectAdaptor.Models.ReadAppointments;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GPConnectAdaptor.ReadAppointments
{
    public class AppointmentsResponseMapper : IAppointmentsResponseMapper
    {
        public List<Appointment> Map(string response)
        {
            List<Appointment> appointments = new List<Appointment>();
            var parsed = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response);

            try
            {
                return parsed.Any(p => p.Key == "entry") ? AppendAppointments(appointments, parsed) : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"unable to parse response when trying to get appointments. Failed with {response}");
            }
        }

        private List<Appointment> AppendAppointments(List<Appointment> appointments, Dictionary<string, dynamic> parsed)
        {
            JArray arrayOfEntries = JsonConvert.DeserializeObject<JArray>(parsed["entry"].ToString());
                        
                        foreach (var entry in arrayOfEntries)
                        {
                            if (entry["resource"]["resourceType"].ToString() == "Appointment")
                            {
                                var appointment = new Appointment()
                                {
                                    Description = entry["resource"]["description"].ToString(),
                                    End = DateTime.Parse(entry["resource"]["end"].ToString()),
                                    Start = DateTime.Parse(entry["resource"]["start"].ToString()),
                                    Location = null,
                                    LocationId = GetId(GetLocation(entry)),
                                    Patient = null,
                                    PatientId = GetId(GetPatient(entry)),
                                    Practitioner = null,
                                    PractitionerId = GetId(GetPractitioner(entry))
                                };
                                
                                appointments.Add(appointment);
                            }
                        }
            
                        return appointments;
        }

        private static int GetId(JToken participant)
        {
            return Int32.Parse(participant["actor"]["reference"].ToString().Split("/")[1]);
        }

        private static JToken GetPatient(JToken entry)
        {
            return entry["resource"]["participant"].First(p => p["actor"]["reference"].ToString().Contains("Patient"));
        }
        
        private static JToken GetPractitioner(JToken entry)
        {
            return entry["resource"]["participant"].First(p => p["actor"]["reference"].ToString().Contains("Practitioner"));
        }
        
        private static JToken GetLocation(JToken entry)
        {
            return entry["resource"]["participant"].First(p => p["actor"]["reference"].ToString().Contains("Location"));
        }
    }
}