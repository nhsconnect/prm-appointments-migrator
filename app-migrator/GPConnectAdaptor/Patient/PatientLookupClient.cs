using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.Patient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GPConnectAdaptor.Patient
{
    public class PatientLookupClient : IPatientLookupClient
    {
        private readonly IPatientLookupHttpClientWrapper _wrapper;

        public PatientLookupClient(IPatientLookupHttpClientWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        public async Task<PatientModel> GetPatient(long nhsNumber)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(await _wrapper.GetAsync(nhsNumber));
                JArray arrayOfEntries = JsonConvert.DeserializeObject<JArray>(response["entry"].ToString());
                var patientJson = arrayOfEntries
                    .First(e => e["resource"]["resourceType"].ToString() == "Patient");

                //return Int32.Parse(patientIdObject);
                
                return new PatientModel()
                {
                    Id = Int32.Parse(patientJson["resource"]["id"].ToString()),
                    Name = patientJson["resource"]["name"].First()["text"].ToString(),
                    NhsNumber = nhsNumber
                };
            }
            catch (JsonReaderException j)
            {
                throw new Exception($"Failed to parse patient response from source system for NHS number : {nhsNumber}");
            }
            catch (Exception e)
            {
                throw new Exception($"Patient response didn't contain id for patient with NHS number : {nhsNumber}. Returned with '{e.Message}'");
            }
        }
    }
}