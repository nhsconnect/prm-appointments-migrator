using System.Collections.Generic;
using GPConnectAdaptor.Models.ReadAppointments;

namespace GPConnectAdaptor.ReadAppointments
{
    public interface IAppointmentsResponseMapper
    {
        List<Appointment> Map(string response);
    }
}