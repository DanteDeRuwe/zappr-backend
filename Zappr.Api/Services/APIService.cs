using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;

namespace Zappr.Api.Services
{
    public abstract class APIService
    {
        protected IConfiguration _configuration;
        protected HttpClient _client = new HttpClient();


        protected HttpResponseMessage GetHttpResponse(string url)
        {
            var responseTask = _client.GetAsync(url);
            responseTask.Wait();

            return responseTask.Result;
        }

        protected string buildUrlWithQueries(string url, Dictionary<string, string> dictionary)
        {
            UriBuilder uriBuilder = new UriBuilder(url) { Port = -1 };
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (var keyValuePair in dictionary)
            {
                query[keyValuePair.Key] = keyValuePair.Value;
            }
            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }

    }
}
