using System.IO;
using System.Reflection;
using FluentAssertions;
using GPConnectAdaptor;
using GPConnectAdaptor.Models;
using GPConnectAdaptor.Models.Jwt;
using Newtonsoft.Json;
using Xunit;

namespace GPConnectAdaptorTests
{
    public class TokenEncoderTests
    {
        private readonly string _file;
        
        public TokenEncoderTests()
        {
            var assembly = typeof(TokenEncoderTests).GetTypeInfo().Assembly;
        
            using (var stream = assembly.GetManifestResourceStream("GPConnectAdaptorTests.TestData.TestJwtToken.json"))
            {
                using (var reader = new StreamReader(stream))
                {
                    _file = reader.ReadToEnd();
                }
            }
        }
        
        [Fact]
        public void Encode_WhenPassedWithCorrectPayload_ReturnsCorrectToken()
        {
            var sut = new TokenEncoder();
        
            var header = "eyJhbGciOiJub25lIiwidHlwIjoiSldUIn0";
        
            var expected =
                header + "." + "UEVORw" +
                "."; // GPConnect Spec requires header, payload and an empty signature object concatenated with .
        
            var result = sut.Encode("PENG");
        
            result.Should().BeEquivalentTo(expected);
        }
        
        [Fact]
        public void Encode_WhenPassedWithHEader_ReturnsCorrectSegment()
        {
            var sut = new TokenEncoder();
        
            // To add indentation in _file using Json convert (encoding is affected by pretty vs non pretty json)
            var jwt = JsonConvert.DeserializeObject<JwtModel>(_file);
            var newFile = JsonConvert.SerializeObject(jwt, Formatting.Indented);
        
            var expected =
                "eyJhbGciOiJub25lIiwidHlwIjoiSldUIn0.ewogICJpc3MiOiAiaHR0cHM6Ly9vcmFuZ2UudGVzdGxhYi5uaHMudWsvIiwKICAic3ViIjogIjEiLAogICJhdWQiOiAiaHR0cHM6Ly9vcmFuZ2UudGVzdGxhYi5uaHMudWsvZ3Bjb25uZWN0LWRlbW9uc3RyYXRvci92MS9maGlyIiwKICAiZXhwIjogMTU4MTA5MTkwOCwKICAiaWF0IjogMTU4MTA5MTYwOCwKICAicmVhc29uX2Zvcl9yZXF1ZXN0IjogImRpcmVjdGNhcmUiLAogICJyZXF1ZXN0ZWRfc2NvcGUiOiAib3JnYW5pemF0aW9uLyoucmVhZCIsCiAgInJlcXVlc3RpbmdfZGV2aWNlIjogewogICAgInJlc291cmNlVHlwZSI6ICJEZXZpY2UiLAogICAgImlkZW50aWZpZXIiOiBbCiAgICAgIHsKICAgICAgICAic3lzdGVtIjogImh0dHBzOi8vb3JhbmdlLnRlc3RsYWIubmhzLnVrL2dwY29ubmVjdC1kZW1vbnN0cmF0b3IvSWQvbG9jYWwtc3lzdGVtLWluc3RhbmNlLWlkIiwKICAgICAgICAidmFsdWUiOiAiZ3BjZGVtb25zdHJhdG9yLTEtb3JhbmdlIgogICAgICB9CiAgICBdLAogICAgIm1vZGVsIjogIkdQIENvbm5lY3QgRGVtb25zdHJhdG9yIiwKICAgICJ2ZXJzaW9uIjogIjEuMi4zIgogIH0sCiAgInJlcXVlc3Rpbmdfb3JnYW5pemF0aW9uIjogewogICAgInJlc291cmNlVHlwZSI6ICJPcmdhbml6YXRpb24iLAogICAgImlkZW50aWZpZXIiOiBbCiAgICAgIHsKICAgICAgICAic3lzdGVtIjogImh0dHBzOi8vZmhpci5uaHMudWsvSWQvb2RzLW9yZ2FuaXphdGlvbi1jb2RlIiwKICAgICAgICAidmFsdWUiOiAiQTExMTExIgogICAgICB9CiAgICBdLAogICAgIm5hbWUiOiAiQ29uc3VtZXIgb3JnYW5pc2F0aW9uIG5hbWUiCiAgfSwKICAicmVxdWVzdGluZ19wcmFjdGl0aW9uZXIiOiB7CiAgICAicmVzb3VyY2VUeXBlIjogIlByYWN0aXRpb25lciIsCiAgICAiaWQiOiAiMSIsCiAgICAiaWRlbnRpZmllciI6IFsKICAgICAgewogICAgICAgICJzeXN0ZW0iOiAiaHR0cHM6Ly9maGlyLm5ocy51ay9JZC9zZHMtdXNlci1pZCIsCiAgICAgICAgInZhbHVlIjogIjExMTExMTExMTExMSIKICAgICAgfSwKICAgICAgewogICAgICAgICJzeXN0ZW0iOiAiaHR0cHM6Ly9maGlyLm5ocy51ay9JZC9zZHMtcm9sZS1wcm9maWxlLWlkIiwKICAgICAgICAidmFsdWUiOiAiMjIyMjIyMjIyMjIyMjIiCiAgICAgIH0sCiAgICAgIHsKICAgICAgICAic3lzdGVtIjogImh0dHBzOi8vb3JhbmdlLnRlc3RsYWIubmhzLnVrL2dwY29ubmVjdC1kZW1vbnN0cmF0b3IvSWQvbG9jYWwtdXNlci1pZCIsCiAgICAgICAgInZhbHVlIjogIjEiCiAgICAgIH0KICAgIF0sCiAgICAibmFtZSI6IFsKICAgICAgewogICAgICAgICJmYW1pbHkiOiAiRGVtb25zdHJhdG9yIiwKICAgICAgICAiZ2l2ZW4iOiBbCiAgICAgICAgICAiR1BDb25uZWN0IgogICAgICAgIF0sCiAgICAgICAgInByZWZpeCI6IFsKICAgICAgICAgICJEciIKICAgICAgICBdCiAgICAgIH0KICAgIF0KICB9Cn0.";
        
            var result = sut.Encode(newFile);
        
            result.Should().BeEquivalentTo(expected);
        }
    }
}