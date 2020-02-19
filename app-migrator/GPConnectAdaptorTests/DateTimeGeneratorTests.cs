using System;
using FluentAssertions;
using GPConnectAdaptor;
using GPConnectAdaptor.Slots;
using Xunit;

namespace GPConnectAdaptorTests
{
    public class DateTimeGeneratorTests
    {
        [Fact]
        public void Generate_WhenPassedWithDateTime_ReturnsUtcString()
        {
            var testInput = new DateTime(2020,02,07,10,00,00);
            var sut = new DateTimeGenerator();
            var result = sut.Generate(testInput);

            result.Should().Be("2020-02-07T10:00:00+00:00");
        }
    }
}