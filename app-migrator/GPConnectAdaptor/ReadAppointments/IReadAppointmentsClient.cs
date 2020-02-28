using System.Collections.Generic;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.ReadAppointments;

namespace GPConnectAdaptor.ReadAppointments
{
    public interface IReadAppointmentsClient
    {
        Task<List<Appointment>> GetFutureAppointments(int patientId);
    }
}