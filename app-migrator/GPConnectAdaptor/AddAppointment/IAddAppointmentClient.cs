using System;
using System.Threading.Tasks;
using GPConnectAdaptor.Models;
using GPConnectAdaptor.Models.AddAppointment;

namespace GPConnectAdaptor
{
    public interface IAddAppointmentClient
    {
        Task<AddAppointmentResponse> AddAppointment(string slotRef,
            string patientRef,
            string locationRef,
            DateTime start,
            DateTime end);
    }
}