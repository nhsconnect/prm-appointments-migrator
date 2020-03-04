using GPConnectAdaptor.Models.AddAppointment;
using GPConnectAdaptor.Patient;

namespace GPConnectAdaptor.AddAppointment
{
    public interface IAppointmentBookedModelMapper
    {
        AppointmentBookedModel Map(string response, IPatientLookup patientLookup);
    }
}