using System;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http.Testing;
using GPConnectAdaptor;
using GPConnectAdaptor.Slots;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace GPConnectAdaptorTests.Slots
{
    public class SlotHttpClientWrapperTests
    {
        private HttpTest _httpTest;
        private readonly ITestOutputHelper _output;
        private readonly IServiceConfig _serviceConfig;

        public SlotHttpClientWrapperTests(ITestOutputHelper output)
        {
            _httpTest = new HttpTest();
            
            this._output = output;
            _serviceConfig = Substitute.For<IServiceConfig>();
            _serviceConfig.GetSourceDomain().Returns("https://www.test.com");
            _serviceConfig.GetTargetDomain().Returns("https://www.test.com");
        }

        [Fact]
        public async Task GetAsync_MakesCorrectCall()
        {
            var mockTokenGenerator = Substitute.For<IJwtTokenGenerator>();
            mockTokenGenerator.GetToken(Scope.OrgRead).Returns("token");
            
            _httpTest.RespondWith("abcd");
            var start = new DateTime(2020, 02, 08, 10, 00, 00);
            var end = new DateTime(2020, 02, 08, 10, 10, 00);
            var sut = new SlotHttpClientWrapper(mockTokenGenerator, new DateTimeGenerator(), _serviceConfig); // isTest = true
        
            var result = await sut.GetSlotsHttp(start, end);
        
            foreach (var call in this._httpTest.CallLog)
            {
                _output.WriteLine(call.ToString());
            }
        
            _httpTest.ShouldHaveMadeACall()
                .WithHeader("Ssp-TraceID", "09a01679-2564-0fb4-5129-aecc81ea2706")
                .WithOAuthBearerToken("token")
                .Times(1);
            
            result.Should().BeEquivalentTo("abcd");
        }
    }
}