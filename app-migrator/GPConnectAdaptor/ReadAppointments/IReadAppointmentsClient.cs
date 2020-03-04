using System.Collections.Generic;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.ReadAppointments;
using GPConnectAdaptor.Patient;

namespace GPConnectAdaptor.ReadAppointments
{
    public interface IReadAppointmentsClient
    {
        Task<List<Appointment>> GetFutureAppointments(int patientId, IPatientLookup patientLookup);
    }
}