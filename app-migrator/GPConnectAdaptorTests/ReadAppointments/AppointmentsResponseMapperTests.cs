using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FluentAssertions;
using GPConnectAdaptor.Models.Patient;
using GPConnectAdaptor.Models.ReadAppointments;
using GPConnectAdaptor.Patient;
using GPConnectAdaptor.ReadAppointments;
using NSubstitute;
using Xunit;

namespace GPConnectAdaptorTests.ReadAppointments
{
    public class AppointmentsResponseMapperTests
    {
        private readonly string[] _filePaths;
        private readonly string[] _files = new string[3];

        public AppointmentsResponseMapperTests()
        {
            var assembly = typeof(AppointmentsResponseMapperTests).GetTypeInfo().Assembly;

            _filePaths = new[]
            {
                "GPConnectAdaptorTests.TestData.ReadAppointmentsTestData.ReadAppointmentResponseWith2Appointments.json",
                "GPConnectAdaptorTests.TestData.ReadAppointmentsTestData.ReadAppointmentsResponseWith0Appointments.json",
                "GPConnectAdaptorTests.TestData.ReadAppointmentsTestData.anothertest.json"
            };
            var i = 0;
            foreach (var filePath in _filePaths)
            {
                using (var stream = assembly.GetManifestResourceStream(filePath))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        _files[i] = (reader.ReadToEnd());
                        i++;
                    }
                }
            }

        }

        [Fact]
        public void Map_WhenFedWithOneAppointment_MapsToAppointment()
        {
            var expected = new Appointment()
            {
                Description = "A appointment to discuss test data",
                End = new DateTime(2020, 02, 29, 09, 10, 00),
                Start = new DateTime(2020, 02, 29, 09, 00, 00),
                Location = null,
                LocationId = 17,
                Practitioner = null,
                PractitionerId = 2,
                Patient = "Mike MEAKIN",
                PatientId = 2
            };

            var mockLookup = Substitute.For<IPatientLookup>();
            mockLookup.GetPatientById(2).Returns(new PatientModel() {Id = 2, Name = "Mike MEAKIN"});
            
            var sut = new AppointmentsResponseMapper();

            var result = sut.Map(_files[0], mockLookup);

            result.Count.Should().Be(2);
            result[0].Should().BeEquivalentTo(expected);
        }
        
        [Fact]
        public void Map_WhenNoAppointmentsFound_ReturnsNull()
        {
            var mockLookup = Substitute.For<IPatientLookup>();
            
            var sut = new AppointmentsResponseMapper();

            var result = sut.Map(_files[1], mockLookup);

            result.Should().BeNull();
        }
        
        [Fact]
        public void Map_anothertest_ReturnsNull()
        {
            var mockLookup = Substitute.For<IPatientLookup>();
            mockLookup.GetPatientById(Arg.Any<int>())
                .Returns(new PatientModel() {Id = 1, Name = "A B", NhsNumber = 101});
            
            var sut = new AppointmentsResponseMapper();

            var result = sut.Map(_files[2], mockLookup);

            result.Count.Should().Be(2);
        }
    }
}