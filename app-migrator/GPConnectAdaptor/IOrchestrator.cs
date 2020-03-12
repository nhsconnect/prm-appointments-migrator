using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GPConnectAdaptor.Models;
using GPConnectAdaptor.Models.AddAppointment;
using GPConnectAdaptor.Models.ReadAppointments;
using GPConnectAdaptor.Models.Slot;
using GPConnectAdaptor.Slots;

namespace GPConnectAdaptor
{
    public interface IOrchestrator
    {
        Task<AppointmentBookedModel> AddAppointment(SlotModel slot, string patientId,
            SourceTarget source = SourceTarget.Target);
        Task<SlotModel> GetSlotInfo(Appointment model, SourceTarget sourceTarget = SourceTarget.Target);
        Task<SlotModel> GetSlotInfo(BookAppointmentModel model, SourceTarget sourceTarget = SourceTarget.Target);
        Task<List<Appointment>> GetFutureAppointments();
    }
}