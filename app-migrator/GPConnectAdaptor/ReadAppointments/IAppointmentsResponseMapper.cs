using System.Collections.Generic;
using GPConnectAdaptor.Models.ReadAppointments;
using GPConnectAdaptor.Patient;
using GPConnectAdaptor.Practitioner;

namespace GPConnectAdaptor.ReadAppointments
{
    public interface IAppointmentsResponseMapper
    {
        List<Appointment> Map(string response, IPatientLookup patientLookup, IPractitionerLookup practitionerLookup);
    }
}