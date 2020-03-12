using System;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http.Testing;
using GPConnectAdaptor;
using GPConnectAdaptor.Patient;
using GPConnectAdaptor.Slots;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace GPConnectAdaptorTests.Patient
{
    public class PatientLookupHttpClientTests
    {
        private HttpTest _httpTest;
        private readonly ITestOutputHelper _output;

        public PatientLookupHttpClientTests(ITestOutputHelper output)
        {
            _httpTest = new HttpTest();
            _output = output;
        }


        // [Fact]
        // public async Task GetAsync_MakesCorrectCall()
        // {
        //     var mockTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        //     mockTokenGenerator.GetToken(Scope.PatientRead).Returns("token");
        //     
        //     _httpTest.RespondWith("abcd");
        //     var sut = new PatientLookupHttpClientWrapper(mockTokenGenerator, true);
        //
        //     var result = await sut.GetAsync(9658218873);
        //
        //     foreach (var call in this._httpTest.CallLog)
        //     {
        //         _output.WriteLine(call.ToString());
        //     }
        //
        //     _httpTest.ShouldHaveMadeACall()
        //         .WithHeader("accept", "application/fhir+json")
        //         .WithHeader("Ssp-From", "200000000359")
        //         .WithHeader("Ssp-To", "918999198993")
        //         .WithHeader("Ssp-TraceID", "09a01679-2564-0fb4-5129-aecc81ea2706")
        //         .WithHeader("Ssp-InteractionID", "urn:nhs:names:services:gpconnect:fhir:rest:search:patient-1")
        //         .WithOAuthBearerToken("token")
        //         .Times(1);
        //     
        //     result.Should().BeEquivalentTo("abcd");
        // }
    }
}