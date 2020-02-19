using System;
using System.Collections.Generic;

namespace GPConnectAdaptor.Models.Slot
{ 
    public class Meta
{
    public DateTime lastUpdated { get; set; }
    public List<string> profile { get; set; }
}

public class Link
{
    public string relation { get; set; }
    public string url { get; set; }
}

public class Meta2
{
    public string versionId { get; set; }
    public DateTime lastUpdated { get; set; }
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
    public string use { get; set; }
}

public class Address
{
    public string use { get; set; }
    public List<string> line { get; set; }
    public string city { get; set; }
    public string district { get; set; }
    public string postalCode { get; set; }
}

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

public class Coding2
{
    public string system { get; set; }
    public string code { get; set; }
    public string display { get; set; }
}

public class ValueCodeableConcept2
{
    public List<Coding2> coding { get; set; }
}

public class Extension2
{
    public string url { get; set; }
    public ValueCodeableConcept2 valueCodeableConcept { get; set; }
}

public class Extension
{
    public string url { get; set; }
    public string valueCode { get; set; }
    public ValueCodeableConcept valueCodeableConcept { get; set; }
    public List<Extension2> extension { get; set; }
}

public class Schedule
{
    public string reference { get; set; }
}

public class Actor
{
    public string reference { get; set; }
}

public class PlanningHorizon
{
    public DateTime start { get; set; }
    public DateTime end { get; set; }
}

public class Resource
{
    public string resourceType { get; set; }
    public string id { get; set; }
    public Meta2 meta { get; set; }
    public List<Identifier> identifier { get; set; }
    public object name { get; set; }
    public List<Telecom> telecom { get; set; }
    public List<Address> address { get; set; }
    public List<Extension> extension { get; set; }
    public Schedule schedule { get; set; }
    public string status { get; set; }
    public DateTime? start { get; set; }
    public DateTime? end { get; set; }
    public List<Actor> actor { get; set; }
    public PlanningHorizon planningHorizon { get; set; }
    public string comment { get; set; }
    public string gender { get; set; }
}

public class Entry
{
    public Resource resource { get; set; }
}

public class SlotResponse
{
    public string resourceType { get; set; }
    public string id { get; set; }
    public Meta meta { get; set; }
    public string type { get; set; }
    public List<Link> link { get; set; }
    public List<Entry> entry { get; set; }
}
}