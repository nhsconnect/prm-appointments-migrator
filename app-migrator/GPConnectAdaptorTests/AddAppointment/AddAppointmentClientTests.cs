using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FluentAssertions;
using GPConnectAdaptor;
using GPConnectAdaptor.AddAppointment;
using GPConnectAdaptor.Models.AddAppointment;
using GPConnectAdaptor.Models.Slot;
using GPConnectAdaptor.Patient;
using GPConnectAdaptor.Slots;
using Hl7.Fhir.Model;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace GPConnectAdaptorTests.AddAppointment
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
            var assembly = typeof(AppointmentBookedModelMapperTests).GetTypeInfo().Assembly;
        
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
        public async Task AddAppointment_WhenAppointmentAvailable_BooksAppointment()
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
        
            var mockMapper = Substitute.For<IAppointmentBookedModelMapper>();
            IPatientLookup mockPatientLookup = Substitute.For<IPatientLookup>();
            mockMapper.Map(_files["success"], mockPatientLookup).Returns(new AppointmentBookedModel()
            {
                Description = "yippee"
            });
            
            var sut = new AddAppointmentClient(mockTokenGenerator, mockRequestBuilder, mockClient, mockMapper);
        
            var result = await sut.AddAppointment(new SlotModel()
                {
                    Id = "1",
                    LocationId = "1",
                    Start = new DateTime(2020, 02, 05, 10, 10, 00),
                    End = new DateTime(2020, 02, 05, 10, 20, 00)
                }, "2",
                SourceTarget.Target,
                mockPatientLookup);
        
            result.Should().NotBeNull();
            result.Description.Should()
                .BeEquivalentTo("yippee");
        }
        
        [Fact]
        public async Task AddAppointment_WhenAppointmentFailsToBook_BooksAppointment()
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
        
            var mockMapper = Substitute.For<IAppointmentBookedModelMapper>();
            IPatientLookup mockPatientLookup = Substitute.For<IPatientLookup>();
            mockMapper.Map(_files["fail"], mockPatientLookup).Returns(new AppointmentBookedModel()
            {
                Success = false,
                Error = "ohnoes"
            });
            
            var sut = new AddAppointmentClient(mockTokenGenerator, mockRequestBuilder, mockClient, mockMapper);
        
            var result = await sut.AddAppointment(new SlotModel()
                {
                    Id = "1",
                    LocationId = "1",
                    Start = new DateTime(2020, 02, 05, 10, 10, 00),
                    End = new DateTime(2020, 02, 05, 10, 20, 00)
                }, "2",
                SourceTarget.Target,
                mockPatientLookup);
        
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Error.Should().BeEquivalentTo("ohnoes");
        }
    }
}