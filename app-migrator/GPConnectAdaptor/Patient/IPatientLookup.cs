using System.Collections.Generic;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.Patient;

namespace GPConnectAdaptor.Patient
{
    public interface IPatientLookup
    {
        Task Initialize(bool isTest = false);
        IEnumerable<int> GetPatientIds();
        PatientModel GetPatientById(int id);

        bool IsInitialized();
    }
}