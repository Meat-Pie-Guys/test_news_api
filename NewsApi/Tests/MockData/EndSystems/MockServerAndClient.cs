using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Tests.MockData.EndSystems
{
    public class MockServerAndClient
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public MockServerAndClient()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(@"C:\Users\jonst\Desktop\Api\NewsApi")
                .UseEnvironment("Development")
                .UseStartup<MockStartUp>()
                .UseApplicationInsights();


            _server = new TestServer(builder);
            _client = _server.CreateClient();
            _client.BaseAddress = new Uri("http://localhost");
        }

        public async Task<MockResponse> Get(string route)
        {
            var x = await _client.GetAsync(route);
            return await Response(x);
        }

        public async Task<MockResponse> Post(string route, HttpContent content)
        {
            return await Response(await _client.PostAsync(route, content));
        }

        public async Task<MockResponse> Put(string route, HttpContent content)
        {
            return await Response(await _client.PutAsync(route, content));
        }

        public async Task<MockResponse> Delete(string route)
        {
            return await Response(await _client.DeleteAsync(route));
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }
        
        private async Task<MockResponse> Response(HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStringAsync();
            var headers = response.Headers;
            
            return new MockResponse
            {
                Code = response.StatusCode,
                Body = body,
                Headers = headers
            };
        }
    }
}