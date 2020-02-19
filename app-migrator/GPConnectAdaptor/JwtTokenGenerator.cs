using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace GPConnectAdaptor
{
	public class JwtTokenGenerator : IJwtTokenGenerator
    {
	    private readonly IEncoder _encoder;
	    private readonly ITokenPayloadBuilder _tokenPayloadBuilder;

	    public JwtTokenGenerator(IEncoder encoder, ITokenPayloadBuilder tokenPayloadBuilder)
	    {
		    _encoder = encoder;
		    _tokenPayloadBuilder = tokenPayloadBuilder;
	    }

	    public string GetToken(Scope scope)
        {
	        var jwt = _tokenPayloadBuilder.BuildPayload(scope);
	        var jsonPayload = JsonConvert.SerializeObject(jwt, Formatting.Indented);
	        var token = _encoder.Encode(jsonPayload);

	        return token;
        }
    }
}