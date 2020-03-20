using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Zappr.Api.Domain;

namespace Zappr.Api.Services
{
    public class TvdbService
    {
        public IConfiguration _configuration;
        public HttpClient _client = new HttpClient();


        public TvdbService(IConfiguration configuration)
        {
            _configuration = configuration;

            _client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", _configuration.GetValue<string>("TVDBToken"));

            _client.DefaultRequestHeaders.Add("Accept-Language", "nl");
        }



        public async Task<Series> GetSeriesByIdAsync(int id)
        {
            var responseTask = _client.GetAsync("https://api.thetvdb.com/series/" + id);
            responseTask.Wait();

            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                string content = await result.Content.ReadAsStringAsync();
                dynamic seriesObj = JsonConvert.DeserializeObject(content);

                //TODO error handling!
                seriesObj = seriesObj.data;
                return ConstructSeries(seriesObj);
            }
            else
            {
                throw new HttpRequestException($"Error in GetSeriesByIdAsync, statuscode: {result.StatusCode}");
            }

        }

        public async Task<List<Series>> SearchSeriesByNameAsync(string name)
        {
            UriBuilder uriBuilder = new UriBuilder("https://api.thetvdb.com/search/series") { Port = -1 };
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["name"] = name;
            uriBuilder.Query = query.ToString();
            string url = uriBuilder.ToString();

            Console.WriteLine("searching series by calling " + url);

            var responseTask = _client.GetAsync(url);
            responseTask.Wait();

            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                string content = await result.Content.ReadAsStringAsync();
                dynamic resObj = JsonConvert.DeserializeObject(content);

                //TODO error handling!
                JArray seriesArr = resObj.data;
                var list = seriesArr.ToObject<List<dynamic>>();

                Console.WriteLine(list.First());

                return list.Select(s => ConstructSeries(s, source: "search") as Series).ToList();
            }
            else
            {
                throw new HttpRequestException($"Error in GetSeriesByIdAsync, statuscode: {result.StatusCode}");
            }

        }

        private Series ConstructSeries(dynamic seriesObj, string source = "")
        {

            Series series = new Series
            {
                Id = seriesObj.id,
                SeriesName = seriesObj.seriesName,
                Description = seriesObj.overview,
                NumberOfSeasons = seriesObj.season,
                Network = seriesObj.network,
                Ended = seriesObj.status == "Ended",
                FirstAired = seriesObj.firstAired,
                Poster = seriesObj.poster,
                Banner = seriesObj.banner,
                Aliases = seriesObj.aliases.ToObject<List<string>>(),
            };

            if (source != "search")
            {
                //when in seach, these fields are not given -_-
                series.AirsTime = seriesObj.airsTime;
                series.Genres = seriesObj.genre.ToObject<List<string>>();

            }

            return series;

        }
    }
}
