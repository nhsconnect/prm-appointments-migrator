using System;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http.Testing;
using GPConnectAdaptor;
using GPConnectAdaptor.AddAppointment;
using GPConnectAdaptor.ReadAppointments;
using GPConnectAdaptor.Slots;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace GPConnectAdaptorTests.ReadAppointments
{
    public class ReadAppointmentsHttpClientWrapperTests
    {
        private HttpTest _httpTest;
        private readonly ITestOutputHelper _output;
        private readonly string _expectedUri = "http://test.com/Patient/2/Appointment?start=ge2020-03-01&start=le2020-03-02";

        public ReadAppointmentsHttpClientWrapperTests(ITestOutputHelper output)
        {
            _httpTest = new HttpTest();
            
            this._output = output;
        }

        [Fact]
        public async Task GetAsync_MakesCorrectCall()
        {
            var mockTokenGenerator = Substitute.For<IJwtTokenGenerator>();
            mockTokenGenerator.GetToken(Scope.PatientRead).Returns("token");
            
            _httpTest.RespondWith("abcd");
            var patientId = 2;
            var start = new DateTime(2020, 03, 01);
            var end = new DateTime(2020, 03, 02);
            var sut = new ReadAppointmentsHttpClientWrapper(mockTokenGenerator, new DateTimeGenerator(), true); // isTest = true

            var result = await sut.GetAsync(start, end, patientId);

            foreach (var call in this._httpTest.CallLog)
            {
                _output.WriteLine(call.ToString());
            }

            _httpTest.ShouldHaveCalled(_expectedUri)
                .WithHeader("accept", "application/fhir+json")
                .WithHeader("Ssp-From", "200000000359")
                .WithHeader("Ssp-To", "918999198993")
                .WithHeader("Ssp-TraceID", "09a01679-2564-0fb4-5129-aecc81ea2706")
                .WithHeader("Ssp-InteractionID", "urn:nhs:names:services:gpconnect:fhir:rest:search:patient_appointments-1")
                .WithOAuthBearerToken("token")
                .Times(1);
            
            result.Should().BeEquivalentTo("abcd");
        }
    }
}