using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace GPConnectAdaptor.Slots
{
    public class SlotHttpClientWrapper : ISlotHttpClientWrapper
    {
        private readonly string _uri = "http://localhost:9000/";
        private readonly string _searchFilter =
            "https://fhir.nhs.uk/STU3/CodeSystem/GPConnect-OrganisationType-1|gp-practice";
        private readonly string _traceId = "09a01679-2564-0fb4-5129-aecc81ea2706";
        private readonly string _consumerAsid = "200000000359";
        private readonly string _providerAsid = "918999198993";
        private readonly string _sdsInteractionId = "urn:nhs:names:services:gpconnect:fhir:rest:search:slot-1";
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IDateTimeGenerator _dateTimeGenerator;

        public SlotHttpClientWrapper(IJwtTokenGenerator tokenGenerator, IDateTimeGenerator dateTimeGenerator)
        {
            _tokenGenerator = tokenGenerator;
            _dateTimeGenerator = dateTimeGenerator;
            FlurlHttp.ConfigureClient(_uri, cli =>
                cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());
        }
        
        public async Task<string> GetAsync(DateTime start, DateTime end)
        {
            var client = SetHeadersAndQueryParam(start, end);

            try
            {
                return await client.GetStringAsync();
            }
            catch (FlurlHttpException f)
            {
                dynamic obj = await f.GetResponseJsonAsync();
                var dict = (IDictionary<string, object>)obj;
                if (dict["resourceType"].Equals("OperationOutcome"))
                {
                    var issue = (IList<object>) dict["issue"];
                    var parsedIssue = (IDictionary<string, object>) issue[0];
                    throw new Exception($"Unable to get slots. Call failed with '{parsedIssue["diagnostics"].ToString()}'");
                }
                
                throw new Exception($"Unable to receive Slots. Unidentified Error");
            }
            
        }

        private IFlurlRequest SetHeadersAndQueryParam(DateTime start, DateTime end)
        {
            return _uri
                .AppendPathSegment("gpconnect-demonstrator/v1/fhir/Slot")
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
}