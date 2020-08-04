using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace Zappr.Infrastructure.Services
{
    public abstract class ApiService
    {
        protected IConfiguration Configuration;
        protected HttpClient Client;

        protected ApiService(IConfiguration configuration, HttpClient client)
        {
            Configuration = configuration;
            Client = client;
        }

        protected HttpResponseMessage GetHttpResponse(string url)
        {
            var responseTask = Client.GetAsync(url);
            responseTask.Wait();
            return responseTask.Result;
        }
    }
}
