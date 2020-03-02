using System.Threading.Tasks;
using GPConnectAdaptor.Slots;

namespace GPConnectAdaptor.AddAppointment
{
    public interface IAddAppointmentHttpClientWrapper
    {
        Task<string> PostAsync(string requestBody, SourceTarget sourceTarget = SourceTarget.Target);
    }
}