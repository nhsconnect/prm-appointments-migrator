using System;
using System.Threading.Tasks;

namespace GPConnectAdaptor.ReadAppointments
{
    public interface IReadAppointmentsHttpClientWrapper
    {
        Task<string> GetAsync(DateTime start, DateTime end, int patientId);
    }
}