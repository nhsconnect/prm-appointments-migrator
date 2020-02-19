using System;
using System.Threading.Tasks;
using GPConnectAdaptor.Models;
using GPConnectAdaptor.Models.AddAppointment;

namespace GPConnectAdaptor
{
    public interface IOrchestrator
    {
        Task<AddAppointmentResponse> AddAppointment(AddAppointmentCriteria criteria);
        Task<AddAppointmentCriteria> GetSlotInfo(TempAddAppointmentRequest request);
    }
}