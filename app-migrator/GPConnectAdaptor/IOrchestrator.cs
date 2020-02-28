using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GPConnectAdaptor.Models;
using GPConnectAdaptor.Models.AddAppointment;
using GPConnectAdaptor.Models.ReadAppointments;

namespace GPConnectAdaptor
{
    public interface IOrchestrator
    {
        Task<AddAppointmentResponse> AddAppointment(AddAppointmentCriteria criteria);
        Task<AddAppointmentCriteria> GetSlotInfo(TempAddAppointmentRequest request);
        Task<List<Appointment>> GetFutureAppointments(List<long> nhsNumbers);
    }
}