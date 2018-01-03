using System.Net;
using System.Net.Http;
using Tests.MockData.EndSystems;
using System.Linq;
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
        public async void Get_ApiNews_Ok()
        {
            // Arrange
            var expected = "[{\"id\":3,\"title\":\"C\",\"releaseDate\":\"2014-04-03T12:44:11\",\"content\":\"_C_\"},{\"id\":4,\"title\":\"D\",\"releaseDate\":\"2015-04-03T10:11:00\",\"content\":\"_D_\"}]";
            // TODO: Replace with some JSON Deserializer and assert on outcome

            // Act
            var response = await _endSystems.Get("/api/news");
    	    
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal(expected, response.Body);
            
            // (CleanUp)
            _endSystems.Dispose();   
        }

        [Fact]  
        public async void Get_ApiNewsWithQuery_Ok()
        {
            // Arrange
            var expected = "[{\"id\":4,\"title\":\"D\",\"releaseDate\":\"2015-04-03T10:11:00\",\"content\":\"_D_\"}]";
            // TODO: Replace with some JSON Deserializer and assert on outcome

            // Act
            var response = await _endSystems.Get("/api/news?year=2015&month=4&day=3");
    	    
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal(expected, response.Body);
            
            // (CleanUp)
            _endSystems.Dispose();   
        }

        [Fact]  
        public async void Get_ApiNewsId_Ok()
        {
            // Arrange
            var expected = "{\"id\":3,\"title\":\"C\",\"releaseDate\":\"2014-04-03T12:44:11\",\"content\":\"_C_\"}";
            // TODO: Replace with some JSON Deserializer and assert on outcome

            // Act
            var response = await _endSystems.Get("/api/news/3");
    	    
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal(expected, response.Body);
            
            // (CleanUp)
            _endSystems.Dispose();   
        }

        [Fact]  
        public async void Get_ApiNewsId_NotFound()
        {
            // Act
            var response = await _endSystems.Get("/api/news/0");
    	    
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.Code);
            
            // (CleanUp)
            _endSystems.Dispose();
        }

        [Fact]
        public async void Post_ApiNews_BadRequest()
        {
            // Arrange
            StringContent content = new StringContent("{}");

            // Act
            var response = await _endSystems.Post("/api/news", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.Code);
            
            // (CleanUp)
            _endSystems.Dispose();
        }

        [Fact]
        public async void Post_ApiNews_Created()
        {
            
            // Arrange
            StringContent content = new StringContent("{\"Title\": \"TestTitle\", \"Content\": \"TestContent\"}");

            // Act
            var response = await _endSystems.Post("/api/news", content);
            var response2 = await _endSystems.Get(response.Headers.Location.AbsolutePath);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.Code);
            Assert.Equal(HttpStatusCode.OK, response2.Code);

            // TODO: Json deserialize
            var x = response2.Body.Substring(1, response2.Body.Length - 2).Split(",").Select(t => t.Replace("\"", "")).ToArray();
            foreach (string y in x)
            {
                var z = y.Split(":");
                if (z[0].ToLower() == "title")
                {
                    Assert.Equal("TestTitle", z[1]);
                }
                if (z[0].ToLower() == "content")
                {
                    Assert.Equal("TestContent", z[1]);
                }
            }
            
            // (CleanUp)
            _endSystems.Dispose();
        }


        [Fact]
        public async void Put_ApiNewsId_NotFound()
        {
            // Arrange
            StringContent content = new StringContent("{\"Title\": \"ChangedTitle\", \"Content\": \"ChangedContent\"}");

            // Act
            var response = await _endSystems.Put("/api/news/0", content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.Code);
        }

        [Fact]
        public async void Put_ApiNewsId_BadRequest()
        {
            // Arrange
            StringContent content = new StringContent("{}");

            // Act
            var response = await _endSystems.Put("/api/news/3", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.Code);
        }

        [Fact]
        public async void Put_ApiNewsId_Ok()
        {
            // Arrange
            StringContent content = new StringContent("{\"Title\": \"ChangedTitle\", \"Content\": \"ChangedContent\"}");
            var expected = "{\"id\":3,\"title\":\"ChangedTitle\",\"releaseDate\":\"2014-04-03T12:44:11\",\"content\":\"ChangedContent\"}";
            // TODO: use Json...

            // Act
            var response = await _endSystems.Put("/api/news/3", content);
            var response2 = await _endSystems.Get("/api/news/3");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.Code);
            Assert.Equal(HttpStatusCode.OK, response2.Code);
            Assert.Equal(expected, response2.Body);
            
            // (CleanUp)
            _endSystems.Dispose();
        }

        [Fact]
        public async void Delete_ApiNewsId_NotFound()
        {
            // Act
            var response = await _endSystems.Delete("/api/news/0");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.Code);
        }

        [Fact]
        public async void Delete_ApiNewsId_NoContent()
        {
            // Act
            var response0 = await _endSystems.Get("api/news/3");
            var response1 = await _endSystems.Delete("/api/news/3");
            var response2 = await _endSystems.Get("api/news/3");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response0.Code);
            Assert.Equal(HttpStatusCode.NoContent, response1.Code);
            Assert.Equal(HttpStatusCode.NotFound, response2.Code);
        }
    }
}