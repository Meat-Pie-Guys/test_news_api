using System;
using Tests.MockData.EndSystems;
using Xunit;

namespace IntegrationTests
{
    public class NewsControllerIntegrationTest
    {
        private readonly MockServerAndClient _endSystems;

        public NewsControllerIntegrationTest()
        {
            _endSystems = new MockServerAndClient();
        }

        [Fact]
        async void foo()
        {
            var response = await _endSystems.Get("/api/news");

            _endSystems.Dispose();
        }
    }
}
