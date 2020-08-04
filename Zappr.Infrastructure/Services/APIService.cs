using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Web;

namespace Zappr.Infrastructure.Services
{
    public abstract class ApiService
    {
        protected IConfiguration Configuration;
        protected HttpClient Client = new HttpClient();

        protected ApiService(IConfiguration configuration) => Configuration = configuration;

        protected HttpResponseMessage GetHttpResponse(string url)
        {
            var responseTask = Client.GetAsync(url);
            responseTask.Wait();
            return responseTask.Result;
        }
    }
}
