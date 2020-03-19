using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FluentAssertions;
using GPConnectAdaptor.AddAppointment;
using GPConnectAdaptor.Models.Patient;
using GPConnectAdaptor.Models.Practitioner;
using GPConnectAdaptor.Patient;
using GPConnectAdaptor.Practitioner;
using Hl7.Fhir.Model;
using NSubstitute;
using Xunit;

namespace GPConnectAdaptorTests.AddAppointment
{
    public class AppointmentBookedModelMapperTests
    {
        private readonly string _appointmentSuccessPath =
            "GPConnectAdaptorTests.TestData.AddAppointmentTestData.AppointmentResponse.json";
        private readonly string _appointmentFailPath =
            "GPConnectAdaptorTests.TestData.AddAppointmentTestData.FailedAppointmentResponse.json";
        private readonly string _jwtFailureAppointmentResponse = 
            "GPConnectAdaptorTests.TestData.AddAppointmentTestData.FailedAppointmentResponse.json";
        private Dictionary<string, string> _filePaths;
        private readonly Dictionary<string, string> _files = new Dictionary<string, string>();
        private readonly IPatientLookup _mockPatientsLookup;
        private readonly IPractitionerLookup _mockPractitionerLookup;

        public AppointmentBookedModelMapperTests()
        {
            PopulatePaths();
            PopulateResponses();
            _mockPatientsLookup = Substitute.For<IPatientLookup>();
            _mockPractitionerLookup = Substitute.For<IPractitionerLookup>();
        }
        
        [Fact]
        public void Deserialize_WhenAppointmentBookingSuccess_ParsesIntoAppointmentResponse()
        {
            var response = _files["success"];
            
            var sut = new AppointmentBookedModelMapper();

            _mockPatientsLookup.GetPatientById(2).Returns(new PatientModel()
                {Id = 2, Name = "Wibble WOBBLE", NhsNumber = 999999999});

            _mockPractitionerLookup.GetPractitionerByLocalId("1")
                .Returns(new PractitionerModel() {Id = 1, Name = "Practitioner One"});

            var result = sut.Map(response, _mockPatientsLookup, _mockPractitionerLookup);

            result.Should().NotBeNull();
            result.Patient.Should().BeEquivalentTo("Wibble WOBBLE");
            result.Description.Should().BeEquivalentTo("A test appointment booked through Interactive Swagger API");
        }

        [Fact]
        public void Deserialize_WhenJwtFails_ParsesIntoAppointmentResponse()
        {
            var response = _files["fail"];
            
            var sut = new AppointmentBookedModelMapper();
            
            
            var result = sut.Map(response, _mockPatientsLookup, _mockPractitionerLookup);

            result.Should().NotBeNull();

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
                },
                {
                    "jwtFail", _jwtFailureAppointmentResponse
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
    }
}