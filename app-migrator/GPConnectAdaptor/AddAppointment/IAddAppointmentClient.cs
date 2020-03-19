using System;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.AddAppointment;
using GPConnectAdaptor.Models.Slot;
using GPConnectAdaptor.Patient;
using GPConnectAdaptor.Practitioner;
using GPConnectAdaptor.Slots;

namespace GPConnectAdaptor.AddAppointment
{
    public interface IAddAppointmentClient
    {
        Task<AppointmentBookedModel> AddAppointment(SlotModel slot,
            string patientRef,
            SourceTarget sourceTarget,
            IPatientLookup patientLookup,
            IPractitionerLookup practitionerLookup);
    }
}