using System;
using System.Collections.Generic;

namespace GPConnectAdaptor.Models.ReadAppointments
{
    public class AppointmentsResponse
    {
        public List<Appointment> Appointments { get; set; }
        
    }

    public class Appointment
    {
        public string Patient { get; set; }
        public int PatientId { get; set; }
        public string Slot { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Location { get; set; }
        public int LocationId { get; set; }
        public string Practitioner { get; set; }
        public int PractitionerId { get; set; }
        public string Description { get; set; }
    }
}