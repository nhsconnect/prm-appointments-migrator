using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPConnectAdaptor.Patient
{
    public interface IPatientLookup
    {
        Task Initialize(List<long> nhsNumbers);
        IEnumerable<int> GetPatientIds();
    }
}