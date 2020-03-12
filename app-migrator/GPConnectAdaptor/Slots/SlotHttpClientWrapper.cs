using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Hl7.Fhir.Language.Debugging;

namespace GPConnectAdaptor.Slots
{
    public class SlotHttpClientWrapper : ISlotHttpClientWrapper
    {
        private readonly string _searchFilter =
            "https://fhir.nhs.uk/STU3/CodeSystem/GPConnect-OrganisationType-1|gp-practice";
        private readonly string _traceId = "09a01679-2564-0fb4-5129-aecc81ea2706";
        private readonly string _consumerAsid = "200000000359";
        private readonly string _providerAsid = "918999198993";
        private readonly string _sdsInteractionId = "urn:nhs:names:services:gpconnect:fhir:rest:search:slot-1";
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IDateTimeGenerator _dateTimeGenerator;
        private readonly IServiceConfig _serviceConfig;

        public SlotHttpClientWrapper(IJwtTokenGenerator tokenGenerator, IDateTimeGenerator dateTimeGenerator, IServiceConfig serviceConfig)
        {
            _tokenGenerator = tokenGenerator;
            _dateTimeGenerator = dateTimeGenerator;
            _serviceConfig = serviceConfig;
        }

        public async Task<string> GetSlotsHttp(DateTime start, DateTime end, SourceTarget sourceTarget = SourceTarget.Target)
        {
            try
            {
                var uri = sourceTarget == SourceTarget.Source
                    ? _serviceConfig.GetSourceDomain()
                    : _serviceConfig.GetTargetDomain();
                
                FlurlHttp.ConfigureClient(uri, cli =>
                    cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());
            
                var client = SetHeadersAndQueryParam(start, end, uri);
                
                var test =  await client.GetStringAsync();

                return test;
            }
            catch (FlurlHttpException f)
            {
                throw new Exception($"Unable to receive Slots. Unidentified Error");
            }
            catch (Exception e)
            {

                throw new Exception($"Unsuccessful call for finding slots");
            }
        }

        private IFlurlRequest SetHeadersAndQueryParam(DateTime start, DateTime end, string uri)
        {
            return uri
                .AppendPathSegment("/Slot")
                .WithHeaders(new
                {
                    Ssp_TraceID = _traceId,
                    Ssp_From = _consumerAsid,
                    Ssp_To = _providerAsid,
                    Ssp_InteractionID = _sdsInteractionId,
                    Accept = "application/fhir+json"
                })
                .WithOAuthBearerToken(_tokenGenerator.GetToken(Scope.OrgRead))
                .SetQueryParams(new
                {
                    start = "ge"+_dateTimeGenerator.Generate(start),
                    end = "le"+_dateTimeGenerator.Generate(end),
                    status = "free",
                    _include = "Slot:schedule"
                })
                .SetQueryParam(Url.Encode("_include:recurse"), "Schedule:actor:Practitioner", false)
                .SetQueryParam("searchFilter", _searchFilter, false);
        }
    }

    public enum SourceTarget
    {
        Source,
        Target,
    }
}