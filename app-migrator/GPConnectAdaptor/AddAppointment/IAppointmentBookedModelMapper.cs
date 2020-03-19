using GPConnectAdaptor.Models.AddAppointment;
using GPConnectAdaptor.Patient;
using GPConnectAdaptor.Practitioner;

namespace GPConnectAdaptor.AddAppointment
{
    public interface IAppointmentBookedModelMapper
    {
        AppointmentBookedModel Map(string response, IPatientLookup patientLookup, IPractitionerLookup practitionerLookup);
    }
}