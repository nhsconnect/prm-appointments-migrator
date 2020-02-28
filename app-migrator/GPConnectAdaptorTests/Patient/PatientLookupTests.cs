using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using GPConnectAdaptor.Patient;
using NSubstitute;
using Xunit;

namespace GPConnectAdaptorTests.Patient
{
    public class PatientLookupTests
    {
        [Fact]
        public async Task Initialise_PopulatesLookup()
        {
            var mockClient = Substitute.For<IPatientLookupClient>();
            mockClient.GetPatientId(1).Returns(1);
            mockClient.GetPatientId(2).Returns(2);
            var testList = new List<long>(){1,2};
            
            var sut = new PatientLookup(mockClient);

            await sut.Initialize(testList);

            sut.Lookup[1].Should().Be(1);
            sut.Lookup[2].Should().Be(2);
        }
    }
}