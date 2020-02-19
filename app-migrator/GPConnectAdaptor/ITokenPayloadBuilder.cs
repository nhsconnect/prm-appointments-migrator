using GPConnectAdaptor.Models;
using GPConnectAdaptor.Models.Jwt;
using JwtModel = GPConnectAdaptor.Models.Jwt.JwtModel;

namespace GPConnectAdaptor
{
    public interface ITokenPayloadBuilder
    {
        JwtModel BuildPayload(Scope scope);
    }
}