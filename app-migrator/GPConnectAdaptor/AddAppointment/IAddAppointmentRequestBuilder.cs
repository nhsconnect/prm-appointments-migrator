using System;
using GPConnectAdaptor.Models.AddAppointment;

namespace GPConnectAdaptor.AddAppointment
{
    public interface IAddAppointmentRequestBuilder
    {
        AddAppointmentRequest Build(string slotRef, string patientRef, string locationRef, DateTime start, DateTime end);
    }
}