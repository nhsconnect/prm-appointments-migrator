using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using GPConnectAdaptor.Slots;

namespace GPConnectAdaptor.AddAppointment
{
    public class AddAppointmentHttpClientWrapper : IAddAppointmentHttpClientWrapper
    {
        private readonly IServiceConfig _serviceConfig;
        private readonly string _traceId = "09a01679-2564-0fb4-5129-aecc81ea2706";
        private readonly string _consumerAsid = "200000000359";
        private readonly string _providerAsid = "918999198993";
        private readonly string _sdsInteractionId = "urn:nhs:names:services:gpconnect:fhir:rest:create:appointment-1";
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AddAppointmentHttpClientWrapper(IJwtTokenGenerator tokenGenerator, IServiceConfig serviceConfig)
        {
            _tokenGenerator = tokenGenerator;
            _serviceConfig = serviceConfig;
            
        }

        public async Task<string> PostAsync(string requestBody, SourceTarget sourceTarget = SourceTarget.Target)
        {
            var uri = sourceTarget == SourceTarget.Source
                ? _serviceConfig.GetSourceDomain()
                : _serviceConfig.GetTargetDomain();
            
            FlurlHttp.ConfigureClient(uri, cli =>
                cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());
            
            var temp = uri
                .AppendPathSegment("/Appointment")
                .WithHeaders(new
                {
                    Ssp_TraceID = _traceId,
                    Ssp_From = _consumerAsid,
                    Ssp_To = _providerAsid,
                    Ssp_InteractionID = _sdsInteractionId,
                    accept = "application/fhir+json",
                    Content_Type = "application/fhir+json"
                    
                })
                .WithOAuthBearerToken(_tokenGenerator.GetToken(Scope.PatientWrite));

            return await temp.AllowAnyHttpStatus().PostStringAsync(requestBody).ReceiveString();
        }
    }
}