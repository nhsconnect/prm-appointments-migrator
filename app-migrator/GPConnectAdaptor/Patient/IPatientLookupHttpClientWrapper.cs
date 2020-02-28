using System;
using System.Threading.Tasks;

namespace GPConnectAdaptor.Patient
{
    public interface IPatientLookupHttpClientWrapper
    {
        Task<string> GetAsync(long nhsNumber);
    }
}