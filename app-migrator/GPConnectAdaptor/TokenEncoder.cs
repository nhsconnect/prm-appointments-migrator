using Microsoft.IdentityModel.Tokens;

namespace GPConnectAdaptor
{
    public class TokenEncoder : IEncoder
    {
        private readonly string _header;

        public TokenEncoder()
        {
            _header = "eyJhbGciOiJub25lIiwidHlwIjoiSldUIn0"; //hardcoded
        }
        public string Encode(string payload)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(payload);
            var encodedPayload =  Base64UrlEncoder.Encode(bytes);

            return _header + "." + encodedPayload + ".";
        }
    }
}