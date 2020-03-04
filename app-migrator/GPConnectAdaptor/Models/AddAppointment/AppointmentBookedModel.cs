using System;

namespace GPConnectAdaptor.Models.AddAppointment
{
    public class AppointmentBookedModel
    {
        public bool Success { get; set; }
        public string Error { get; set; }
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