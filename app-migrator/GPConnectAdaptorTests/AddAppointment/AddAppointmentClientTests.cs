using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using GPConnectAdaptor;
using GPConnectAdaptor.AddAppointment;
using GPConnectAdaptor.Models;
using GPConnectAdaptor.Models.AddAppointment;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;

namespace GPConnectAdaptorTests
{
    public class AddAppointmentClientTests
    {
        private readonly string _appointmentSuccessPath =
            "GPConnectAdaptorTests.TestData.AddAppointmentTestData.AppointmentResponse.json";
        private readonly string _appointmentFailPath =
            "GPConnectAdaptorTests.TestData.AddAppointmentTestData.FailedAppointmentResponse.json";
        private Dictionary<string, string> _filePaths;
        private readonly Dictionary<string, string> _files = new Dictionary<string, string>();

        public AddAppointmentClientTests()
        {
            PopulatePaths();
            PopulateResponses();
        }
        
        
        private void PopulatePaths()
        {
            _filePaths = new Dictionary<string, string>()
            {
                {
                    "success", _appointmentSuccessPath
                },
                {
                    "fail", _appointmentFailPath
                }
            };
        }

        private void PopulateResponses()
        {
            var assembly = typeof(AddAppointmentResponseDeserializerTests).GetTypeInfo().Assembly;

            foreach (var filePath in _filePaths)
            {
                using (var stream = assembly.GetManifestResourceStream(filePath.Value))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        _files.Add(filePath.Key, reader.ReadToEnd());
                    }
                }
            }
        }
        
        [Fact]
        public void AddAppointment_WhenAppointmentAvailable_BooksAppointment()
        {
            var mockTokenGenerator = Substitute.For<IJwtTokenGenerator>();
            mockTokenGenerator.GetToken(Scope.PatientWrite).Returns("token");

            var mockRequestBuilder = Substitute.For<IAddAppointmentRequestBuilder>();
            mockRequestBuilder
                .Build(
                    "1",
                    "2",
                    "1",
                    new DateTime(2020, 02, 05, 10, 10, 00),
                    new DateTime(2020, 02, 05, 10, 20, 00))
                .Returns(new AddAppointmentRequest());

            var mockClient = Substitute.For<IAddAppointmentHttpClientWrapper>();
            mockClient.PostAsync(Arg.Any<string>()).Returns(_files["success"]);

            var mockDeserializer = Substitute.For<IAddAppointmentResponseDeserializer>();
            mockDeserializer.Deserialize(_files["success"]).Returns(JsonConvert.DeserializeObject<AddAppointmentResponse>(_files["success"]));
            
            var sut = new AddAppointmentClient(mockTokenGenerator, mockRequestBuilder, mockClient, mockDeserializer);

            var result = sut.AddAppointment("1",
                "2",
                "1",
                new DateTime(2020, 02, 05, 10, 10, 00),
                new DateTime(2020, 02, 05, 10, 20, 00));

            result.Should().NotBeNull();
            result.Result.description.Should()
                .BeEquivalentTo("A test appointment booked through Interactive Swagger API");
        }
        
        [Fact]
        public void AddAppointment_WhenAppointmentNotAvailable_ReturnsOutcome()
        {
            var mockTokenGenerator = Substitute.For<IJwtTokenGenerator>();
            mockTokenGenerator.GetToken(Scope.PatientWrite).Returns("token");

            var mockRequestBuilder = Substitute.For<IAddAppointmentRequestBuilder>();
            mockRequestBuilder
                .Build(
                    "1",
                    "2",
                    "1",
                    new DateTime(2020, 02, 05, 10, 10, 00),
                    new DateTime(2020, 02, 05, 10, 20, 00))
                .Returns(new AddAppointmentRequest());

            var mockClient = Substitute.For<IAddAppointmentHttpClientWrapper>();
            mockClient.PostAsync(Arg.Any<string>()).Returns(_files["fail"]);

            var mockDeserializer = Substitute.For<IAddAppointmentResponseDeserializer>();
            mockDeserializer.Deserialize(_files["fail"]).Returns(JsonConvert.DeserializeObject<AddAppointmentResponse>(_files["fail"]));
            
            var sut = new AddAppointmentClient(mockTokenGenerator, mockRequestBuilder, mockClient, mockDeserializer);

            var result = sut.AddAppointment("1",
                "2",
                "1",
                new DateTime(2020, 02, 05, 10, 10, 00),
                new DateTime(2020, 02, 05, 10, 20, 00));

            result.Should().NotBeNull();
            result.Result.resourceType.Should().BeEquivalentTo("OperationOutcome");
        }
    }
}