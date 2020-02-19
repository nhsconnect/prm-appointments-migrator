using System.IO;
using System.Reflection;
using FluentAssertions;
using GPConnectAdaptor;
using GPConnectAdaptor.AddAppointment;
using Xunit;

namespace GPConnectAdaptorTests
{
    public class AddAppointmentRequestDeserializerTests
    {
        private readonly string _file;
        
        public AddAppointmentRequestDeserializerTests()
        { 
            var assembly = typeof(AddAppointmentRequestDeserializerTests).GetTypeInfo().Assembly;
            
            using (var stream = assembly.GetManifestResourceStream("GPConnectAdaptorTests.TestData.AddAppointmentTestData.TestAddAppointmentRequest.json"))
            {
                using (var reader = new StreamReader(stream))
                {
                    _file = reader.ReadToEnd();
                }
            }
        
        }

        [Fact]
        public void Deserialize_WhenPassedWithValidJson_CreatesRequest()
        {
            var sut = new AddAppointmentRequestDeserializer();

            var result = sut.Deserialize(_file);

            result.Should().NotBeNull();
            result.slot.Count.Should().Be(1);
            result.description.Should().BeEquivalentTo("A test appointment booked through Interactive Swagger API");
        }
    }
}