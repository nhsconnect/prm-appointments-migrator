using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GPConnectAdaptor.Models.AddAppointment;

namespace GPConnectAdaptor.AddAppointment
{
    public class AddAppointmentRequestBuilder : IAddAppointmentRequestBuilder
    {
        private readonly Assembly _assembly = typeof(AddAppointmentRequestBuilder).GetTypeInfo().Assembly;
        private readonly string _template;
        private readonly IAddAppointmentRequestDeserializer _addAppointmentRequestDeserializer;

        public AddAppointmentRequestBuilder(IAddAppointmentRequestDeserializer addAppointmentRequestDeserializer)
        {
            _addAppointmentRequestDeserializer = addAppointmentRequestDeserializer;
            
            using (var stream = _assembly.GetManifestResourceStream("GPConnectAdaptor.Templates.AddAppointmentRequestTemplate.json"))
            {
                using (var reader = new StreamReader(stream))
                {
                    _template = reader.ReadToEnd();
                }
            }
        }

        public AddAppointmentRequest Build(string slotRef, string patientRef, string locationRef, DateTime start, DateTime end)
        {
            var baseRequest = _addAppointmentRequestDeserializer.Deserialize(_template);

            baseRequest.start = start;
            baseRequest.end = end;
            baseRequest.created = DateTime.Now;
            baseRequest.slot = new List<Slot>();
            baseRequest.slot.Add(new Slot(){reference = "Slot/"+slotRef});
            baseRequest.participant = new List<Participant>();
            baseRequest.participant.Add(new Participant()
            {
                actor = new Actor(){reference = "Patient/" + patientRef},
                status = "accepted"
            });
            baseRequest.participant.Add(new Participant()
            {
                actor = new Actor(){reference = "Location/" + locationRef},
                status = "accepted"
            });

            return baseRequest;
        }
    }
}