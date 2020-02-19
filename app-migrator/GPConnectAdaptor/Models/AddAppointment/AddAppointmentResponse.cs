using System;
using System.Collections.Generic;

namespace GPConnectAdaptor.Models.AddAppointment
{
        public class Coding
    {
        public string system { get; set; }
        public string code { get; set; }
        public string display { get; set; }
    }

    public class ValueCodeableConcept
    {
        public List<Coding> coding { get; set; }
    }

    public class AddAppointmentResponse
    {
        public string resourceType { get; set; }
        public string id { get; set; }
        public Meta meta { get; set; }
        public List<Contained> contained { get; set; }
        public List<Extension> extension { get; set; }
        public string status { get; set; }
        public int priority { get; set; }
        public string description { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int minutesDuration { get; set; }
        public List<Slot> slot { get; set; }
        public DateTime created { get; set; }
        public List<Participant> participant { get; set; }
    }

}