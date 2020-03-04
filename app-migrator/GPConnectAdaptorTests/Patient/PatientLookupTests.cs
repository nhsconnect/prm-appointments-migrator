using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using GPConnectAdaptor.Models.Patient;
using GPConnectAdaptor.Patient;
using Microsoft.VisualBasic.CompilerServices;
using NSubstitute;
using Xunit;

namespace GPConnectAdaptorTests.Patient
{
    public class PatientLookupTests
    {
        [Fact]
        public async Task Initialise_PopulatesLookup()
        {
            var patient1 = new PatientModel() {Id = 1, Name = "Patient A"};
            var patient2 = new PatientModel() {Id = 2, Name = "Patient B"};
            
            var mockClient = Substitute.For<IPatientLookupClient>();
            mockClient.GetPatient(9658218865).Returns(patient1);
            mockClient.GetPatient(9658218873).Returns(patient2);

            var sut = new PatientLookup(mockClient);

            await sut.Initialize(true);

            sut.Lookup[9658218865].Should().Be(patient1);
            sut.Lookup[9658218873].Should().Be(patient2);
        }
        
        [Fact]
        public async Task GetPatientById_Works()
        {
            var patient1 = new PatientModel() {Id = 1, Name = "Patient A", NhsNumber = 9658218865};
            var patient2 = new PatientModel() {Id = 2, Name = "Patient B", NhsNumber = 9658218873};
            
            var mockClient = Substitute.For<IPatientLookupClient>();
            mockClient.GetPatient(9658218865).Returns(patient1);
            mockClient.GetPatient(9658218873).Returns(patient2);
            var testList = new List<long>(){9658218865,9658218873};

            var sut = new PatientLookup(mockClient);
            
            await sut.Initialize(true);

            sut.GetPatientById(1).Should().Be(patient1);
            sut.GetPatientById(2).Should().Be(patient2);
        }
    }
}