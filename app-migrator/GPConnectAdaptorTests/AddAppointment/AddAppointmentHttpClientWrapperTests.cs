using FluentAssertions;
using Flurl.Http.Testing;
using GPConnectAdaptor;
using GPConnectAdaptor.AddAppointment;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace GPConnectAdaptorTests.AddAppointment
{
    public class AddAppointmentHttpClientWrapperTests
    {
        private readonly HttpTest _httpTest;
        private readonly ITestOutputHelper _output;
        //private readonly string _expectedUri = "http://test.com/Appointment";
        private readonly IServiceConfig _serviceConfig;


        public AddAppointmentHttpClientWrapperTests(ITestOutputHelper output)
        {
            _output = output;
            _httpTest = new HttpTest();
            _serviceConfig = Substitute.For<IServiceConfig>();
            _serviceConfig.GetSourceDomain().Returns("http://test.com");
            _serviceConfig.GetTargetDomain().Returns("http://test.com");
        }
        
        [Fact]
        public async void PostAsync_MakesCorrectRequest()
        {
            var mockTokenGenerator = Substitute.For<IJwtTokenGenerator>();
            mockTokenGenerator.GetToken(Scope.PatientWrite).Returns("token");
            var mockRequestBody = "{\"hello\" : \"hello\"}";
            _httpTest.RespondWith("{\"aha!\" : \"aha!\"}");
        
            var sut = new AddAppointmentHttpClientWrapper(mockTokenGenerator, _serviceConfig);
        
            var result = await sut.PostAsync(mockRequestBody);
        
            foreach (var call in this._httpTest.CallLog)
            {
                _output.WriteLine(call.ToString());
            }
        
            _httpTest.ShouldHaveMadeACall();
        
            _httpTest.ShouldHaveMadeACall()
                .WithRequestBody("{\"hello\" : \"hello\"}")
                .WithHeader("Ssp-TraceID", "09a01679-2564-0fb4-5129-aecc81ea2706")
                .WithHeader("Ssp-From", "200000000359")
                .WithHeader("Ssp-To", "918999198993")
                .WithHeader("Ssp-InteractionID", "urn:nhs:names:services:gpconnect:fhir:rest:create:appointment-1")
                .WithHeader("Content-Type", "application/fhir+json")
                .WithHeader("accept", "application/fhir+json")
                .WithOAuthBearerToken("token")
                .Times(1);
            
            result.Should().BeEquivalentTo("{\"aha!\" : \"aha!\"}");
        }
    }
}