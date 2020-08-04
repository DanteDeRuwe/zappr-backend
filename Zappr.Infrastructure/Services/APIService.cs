using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        protected static string BuildUrlWithQueries(string url, Dictionary<string, string> dictionary)
        {
            var uriBuilder = new UriBuilder(url) { Port = -1 };
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach ((string key, string value) in dictionary) query[key] = value;

            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }

    }
}
