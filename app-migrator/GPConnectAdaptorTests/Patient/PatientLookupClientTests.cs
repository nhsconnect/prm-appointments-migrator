using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using GPConnectAdaptor.Patient;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;

namespace GPConnectAdaptorTests.Patient
{
    public class PatientLookupClientTests
    {
        private readonly string[] _filePaths;
        private readonly string[] _files = new string[1];

        public PatientLookupClientTests()
        {
            var assembly = typeof(PatientLookupClientTests).GetTypeInfo().Assembly;

            _filePaths = new[]
            {
                "GPConnectAdaptorTests.TestData.PatientTestData.PositivePatientLookupResponse.json"
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
        public async Task GetPatientId_WhenReceivedInResponse_ReturnsId()
        {
            var mockWrapper = Substitute.For<IPatientLookupHttpClientWrapper>();
            mockWrapper.GetAsync(1).Returns(_files[0]);
            var sut = new PatientLookupClient(mockWrapper);

            var result = await sut.GetPatientId(1);

            result.Should().Be(2);
        } 
        
        [Fact]
        public async Task GetPatientId_UnableToParse_Throws()
        {
            var mockWrapper = Substitute.For<IPatientLookupHttpClientWrapper>();
            mockWrapper.GetAsync(1).Returns("eirbveiuwrbv");
            var sut = new PatientLookupClient(mockWrapper);

            JsonReaderException ex =  await Assert.ThrowsAsync<JsonReaderException>(() => sut.GetPatientId(1));
        } 
    }
}