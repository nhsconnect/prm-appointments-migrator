using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FluentAssertions;
using GPConnectAdaptor.Models.ReadAppointments;
using GPConnectAdaptor.ReadAppointments;
using Xunit;

namespace GPConnectAdaptorTests.ReadAppointments
{
    public class AppointmentsResponseMapperTests
    {
        private readonly string[] _filePaths;
        private readonly string[] _files = new string[2];

        public AppointmentsResponseMapperTests()
        {
            var assembly = typeof(AppointmentsResponseMapperTests).GetTypeInfo().Assembly;

            _filePaths = new[]
            {
                "GPConnectAdaptorTests.TestData.ReadAppointmentsTestData.ReadAppointmentResponseWith2Appointments.json",
                "GPConnectAdaptorTests.TestData.ReadAppointmentsTestData.ReadAppointmentsResponseWith0Appointments.json"
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
                Patient = null,
                PatientId = 2
            };
            
            var sut = new AppointmentsResponseMapper();

            var result = sut.Map(_files[0]);

            result.Count.Should().Be(2);
            result[0].Should().BeEquivalentTo(expected);
        }
        
        [Fact]
        public void Map_WhenNoAppointmentsFound_ReturnsNull()
        {
            var sut = new AppointmentsResponseMapper();

            var result = sut.Map(_files[1]);

            result.Should().BeNull();
        }
    }
}