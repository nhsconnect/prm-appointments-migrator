using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GPConnectAdaptor.Models;
using GPConnectAdaptor.Models.AddAppointment;
using GPConnectAdaptor.Models.ReadAppointments;
using GPConnectAdaptor.Slots;

namespace GPConnectAdaptor
{
    public interface IOrchestrator
    {
        Task<AddAppointmentResponse> AddAppointment(AddAppointmentCriteria criteria, SourceTarget source = SourceTarget.Target);
        Task<AddAppointmentCriteria> GetSlotInfo(BookAppointmentModel model, SourceTarget sourceTarget = SourceTarget.Target);
        Task<List<Appointment>> GetFutureAppointments(List<long> nhsNumbers);
    }
}