using System.Net;
using System.Net.Http.Headers;

namespace Tests.MockData.EndSystems
{
    public class MockResponse
    {
        public HttpStatusCode Code { get; set; }
        public HttpResponseHeaders Headers { get; set; }
        public string Body { get; set; }
    }
}