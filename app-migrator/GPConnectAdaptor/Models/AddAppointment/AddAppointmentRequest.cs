using System;
using System.Collections.Generic;

namespace GPConnectAdaptor.Models.AddAppointment
{
    public class Meta
    {
        public List<string> profile { get; set; }
    }

    public class Meta2
    {
        public List<string> profile { get; set; }
    }

    public class Identifier
    {
        public string system { get; set; }
        public string value { get; set; }
    }

    public class Telecom
    {
        public string system { get; set; }
        public string value { get; set; }
    }

    public class Contained
    {
        public string resourceType { get; set; }
        public string id { get; set; }
        public Meta2 meta { get; set; }
        public List<Identifier> identifier { get; set; }
        public string name { get; set; }
        public List<Telecom> telecom { get; set; }
    }

    public class Slot
    {
        public string reference { get; set; }
    }

    public class Actor
    {
        public string reference { get; set; }
    }

    public class Participant
    {
        public Actor actor { get; set; }
        public string status { get; set; }
    }

    public class ValueReference
    {
        public string reference { get; set; }
    }

    public class Extension
    {
        public string url { get; set; }
        public ValueReference valueReference { get; set; }
    }
    
    public class AddAppointmentRequest
    {
        public string resourceType { get; set; }
        public Meta meta { get; set; }
        public List<Contained> contained { get; set; }
        public string status { get; set; }
        public DateTime created { get; set; }
        public string description { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public List<Slot> slot { get; set; }
        public List<Participant> participant { get; set; }
        public List<Extension> extension { get; set; }
    }
}