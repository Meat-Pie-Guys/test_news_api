using System.Net;
using Tests.MockData.EndSystems;
using Xunit;

namespace Tests
{
    public class NewsIntegrationTest
    {
        private readonly MockServerAndClient _endSystems;

        public NewsIntegrationTest()
        {
            _endSystems = new MockServerAndClient();
        }

        [Fact]  
        public async void Get_ApiNews_Sucess()
        {
            // Arrange
            var expected = "[{\"id\":2,\"title\":\"B\",\"releaseDate\":\"2015-04-03T12:44:11\",\"content\":\"_B_\"},{\"id\":1,\"title\":\"A\",\"releaseDate\":\"2015-04-03T12:44:39\",\"content\":\"_A_\"}]";
            // TODO: Replace with some JSON Deserializer

            // Act
            var response = await _endSystems.Get("/api/news");
    	    
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal(expected, response.Body);
            
            // (CleanUp)
            _endSystems.Dispose();   
        }
    }
}