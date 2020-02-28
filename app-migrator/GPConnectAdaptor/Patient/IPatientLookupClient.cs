using System.Threading.Tasks;

namespace GPConnectAdaptor.Patient
{
    public interface IPatientLookupClient
    {
        Task<int> GetPatientId(long nhsNumber);
    }
}