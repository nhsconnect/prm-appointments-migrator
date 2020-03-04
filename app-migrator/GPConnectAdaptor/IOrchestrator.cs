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
        Task<AppointmentBookedModel> AddAppointment(AddAppointmentCriteria criteria,
            SourceTarget source = SourceTarget.Target);
        Task<AddAppointmentCriteria> GetSlotInfo(Appointment model, SourceTarget sourceTarget = SourceTarget.Target);
        Task<AddAppointmentCriteria> GetSlotInfo(BookAppointmentModel model, SourceTarget sourceTarget = SourceTarget.Target);
        Task<List<Appointment>> GetFutureAppointments();
        Task<List<AddAppointmentResponse>> TransferAppointments(List<Appointment> model);
    }
}