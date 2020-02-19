using System.Threading.Tasks;
using GPConnectAdaptor.Models.AddAppointment;

namespace GPConnectAdaptor
{
    public interface IAddAppointmentHttpClientWrapper
    {
        Task<string> PostAsync(string requestBody);
    }
}