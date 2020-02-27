using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.ReadAppointments;

namespace GPConnectAdaptor.Patient
{
    public class PatientLookup : IPatientLookup
    {
        private readonly IPatientLookupClient _client;

        public Dictionary<long, int> Lookup { get; private set; }

        public PatientLookup(IPatientLookupClient client)
        {
            _client = client;
        }

        public async Task Initialize(List<long> nhsNumbers)
        {
            Lookup = new Dictionary<long, int>();
            
            foreach (var nhsNumber in nhsNumbers)
            {
                Lookup.Add(nhsNumber, await _client.GetPatientId(nhsNumber));
            }
        }

        public IEnumerable<int> GetPatientIds()
        {
            foreach (var entry in Lookup)
            {
                yield return entry.Value;
            }
        }
    }
}