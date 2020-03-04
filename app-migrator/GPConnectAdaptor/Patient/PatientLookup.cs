using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.Patient;
using GPConnectAdaptor.Models.ReadAppointments;

namespace GPConnectAdaptor.Patient
{
    public class PatientLookup : IPatientLookup
    {
        private readonly IPatientLookupClient _client;

        public Dictionary<long, PatientModel> Lookup { get; private set; }
        public Dictionary<int, long> LookupNhsNumberById { get; set; }
        
        public bool Initialized { get; set; }

        public PatientLookup(IPatientLookupClient client)
        {
            _client = client;
        }

        public async Task Initialize(bool isTest = false)
        {
            Lookup = new Dictionary<long, PatientModel>();
            LookupNhsNumberById = new Dictionary<int, long>();
            var nhsNumbers = isTest ? new List<long>() {9658218865, 9658218873} : GetHardcodedNhsNumbers();
            
            foreach (var nhsNumber in nhsNumbers)
            {
                var patient = await _client.GetPatient(nhsNumber);
                Lookup.Add(nhsNumber, patient);
                LookupNhsNumberById.Add(patient.Id, nhsNumber);
            }

            Initialized = true;
        }

        public IEnumerable<int> GetPatientIds()
        {
            foreach (var pair in Lookup)
            {
                yield return pair.Value.Id;
            }
        }

        public PatientModel GetPatientById(int id)
        {
            var nhsNumber = LookupNhsNumberById[id];
            return Lookup[nhsNumber];
        }

        public bool IsInitialized()
        {
            return Initialized;
        }
        
        private List<long> GetHardcodedNhsNumbers()
        {
            return new List<long>()
            {
                9658218865,
                9658218873,
                9658218881,
                9658218903,
                9658218989,
                9658218997,
                9658219004,
                9658219012
            };
        }
    }
}