using System;
using System.Linq;
using FluentAssertions;
using GPConnectAdaptor;
using GPConnectAdaptor.AddAppointment;
using GPConnectAdaptor.Models.AddAppointment;
using NSubstitute;
using Xunit;

namespace GPConnectAdaptorTests
{
    public class AddAppointmentRequestBuilderTests
    {

        [Fact]
        public void Build_WhenPassedWithParameters_BuildRequestWithParameters()
        {
            var mockDeserializer = Substitute.For<IAddAppointmentRequestDeserializer>();
            mockDeserializer.Deserialize(Arg.Any<string>()).Returns(new AddAppointmentRequest());
            
            var start = DateTime.Now;
            var end = start.AddMinutes(10);
            
            var sut = new AddAppointmentRequestBuilder(mockDeserializer);
            
            var result = sut.Build("1", "2", "1", start, end);

            result.start.Should().BeCloseTo(start, new TimeSpan(0, 0, 0, 10));
            result.end.Should().BeCloseTo(end, new TimeSpan(0, 0, 0, 10));
            result.created.Should().BeCloseTo(DateTime.Now, new TimeSpan(0, 0, 0, 10));
            result.slot[0].reference.Should().BeEquivalentTo("Slot/1");
            result.participant.Count(p => p.actor.reference == "Patient/2").Should().Be(1);
            result.participant.Count(p => p.actor.reference == "Location/1").Should().Be(1);
        }
    }
}