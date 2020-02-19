using System;
using GPConnectAdaptor.Models;
using GPConnectAdaptor.Models.AddAppointment;

namespace GPConnectAdaptor
{
    public interface IAddAppointmentRequestBuilder
    {
        AddAppointmentRequest Build(string slotRef, string patientRef, string locationRef, DateTime start, DateTime end);
    }
}