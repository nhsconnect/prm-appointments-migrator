using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using GPConnectAdaptor.Slots;

namespace GPConnectAdaptor.ReadAppointments
{
    public class ReadAppointmentsHttpClientWrapper : IReadAppointmentsHttpClientWrapper
    {
        
        private readonly string _uri;
        private readonly string _traceId = "09a01679-2564-0fb4-5129-aecc81ea2706";
        private readonly string _consumerAsid = "200000000359";
        private readonly string _providerAsid = "918999198993";
        private readonly string _sdsInteractionId = "urn:nhs:names:services:gpconnect:fhir:rest:search:patient_appointments-1";
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IDateTimeGenerator _dateTimeGenerator;

        public ReadAppointmentsHttpClientWrapper(IJwtTokenGenerator tokenGenerator, IDateTimeGenerator dateTimeGenerator, bool isTest = false)
        {
            _tokenGenerator = tokenGenerator;
            _dateTimeGenerator = dateTimeGenerator;
            _uri =   ServiceConfig.GetSourceDomain(); //isTest ? "http://test.com" :
            FlurlHttp.ConfigureClient(_uri, cli =>
                cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());
        }
        
        public async Task<string> GetAsync(DateTime start, DateTime end, int patientId)
        {
            var client = SetHeadersAndQueryParam(start, end, patientId);

            try
            {
                return await client.GetStringAsync();
            }
            catch (FlurlHttpException e)
            {
                throw new Exception($"Failed to find patient with id {patientId}. Returned with message '{e.Message}'");
            }
        }
        
        private IFlurlRequest SetHeadersAndQueryParam(DateTime start, DateTime end, int patientId)
        {
            return _uri
                .AppendPathSegment($"/Patient/" + patientId+ "/Appointment")
                .WithHeaders(new
                {
                    Ssp_TraceID = _traceId,
                    Ssp_From = _consumerAsid,
                    Ssp_To = _providerAsid,
                    Ssp_InteractionID = _sdsInteractionId,
                    Accept = "application/fhir+json"
                })
                .WithOAuthBearerToken(_tokenGenerator.GetToken(Scope.PatientRead))
                .SetQueryParams(new
                {
                    start = new string[2]
                    {
                        "ge" + _dateTimeGenerator.GenerateDate(start),
                        "le" + _dateTimeGenerator.GenerateDate(end)
                    },

                });
        }
    }
}