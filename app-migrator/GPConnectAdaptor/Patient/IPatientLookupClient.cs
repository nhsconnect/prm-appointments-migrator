using System.Threading.Tasks;
using GPConnectAdaptor.Models.Patient;

namespace GPConnectAdaptor.Patient
{
    public interface IPatientLookupClient
    {
        Task<PatientModel> GetPatient(long nhsNumber);
    }
}