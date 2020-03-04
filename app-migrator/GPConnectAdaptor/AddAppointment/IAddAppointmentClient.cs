using System;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.AddAppointment;
using GPConnectAdaptor.Patient;
using GPConnectAdaptor.Slots;

namespace GPConnectAdaptor.AddAppointment
{
    public interface IAddAppointmentClient
    {
        Task<AppointmentBookedModel> AddAppointment(string slotRef,
            string patientRef,
            string locationRef,
            DateTime start,
            DateTime end,
            SourceTarget sourceTarget,
            IPatientLookup patientLookup);
    }
}