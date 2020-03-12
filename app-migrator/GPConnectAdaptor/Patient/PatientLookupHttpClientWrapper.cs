using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using GPConnectAdaptor.Slots;

namespace GPConnectAdaptor.Patient
{
    public class PatientLookupHttpClientWrapper : IPatientLookupHttpClientWrapper
    {
        private readonly string _uri;
        private readonly IServiceConfig _serviceConfig;
        private readonly string _traceId = "09a01679-2564-0fb4-5129-aecc81ea2706";
        private readonly string _consumerAsid = "200000000359";
        private readonly string _providerAsid = "918999198993";
        private readonly string _sdsInteractionId = "urn:nhs:names:services:gpconnect:fhir:rest:search:patient-1";
        private readonly IJwtTokenGenerator _tokenGenerator;

        public PatientLookupHttpClientWrapper(IJwtTokenGenerator tokenGenerator, IServiceConfig serviceConfig)
        {
            _tokenGenerator = tokenGenerator;
            _serviceConfig = serviceConfig;
            _uri = _serviceConfig.GetSourceDomain();
            FlurlHttp.ConfigureClient(_uri, cli =>
                cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());
        }
        
        public async Task<string> GetAsync(long nhsNumber)
        {
            var client = SetHeadersAndQueryParam(nhsNumber);

            try
            {
                return await client.GetStringAsync();
            }
            catch (FlurlHttpException e)
            {
                throw new Exception($"Failed to find patient with NHS number : {nhsNumber}. Returned with message '{e.Message}'");
            }
        }
        
        private IFlurlRequest SetHeadersAndQueryParam(long number)
        {
            return _uri
                .AppendPathSegment("/Patient")
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
                    identifier = "https://fhir.nhs.uk/Id/nhs-number|" + number
                });
        }
    }
}