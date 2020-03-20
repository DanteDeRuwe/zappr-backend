using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Zappr.Api.Domain;

namespace Zappr.Api.Services
{
    public class TvdbService
    {
        public IConfiguration _configuration;


        public TvdbService(IConfiguration configuration) => _configuration = configuration;



        public async Task<Series> GetSeriesByIdAsync(int id)
        {
            using HttpClient client = new HttpClient();

            System.Console.WriteLine(_configuration.GetValue<string>("TVDBToken"));

            client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", _configuration.GetValue<string>("TVDBToken"));

            client.DefaultRequestHeaders.Add("Accept-Language", "nl");

            Task<HttpResponseMessage> responseTask = client.GetAsync("https://api.thetvdb.com/series/" + id);
            responseTask.Wait();

            HttpResponseMessage result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                string content = await result.Content.ReadAsStringAsync();
                dynamic seriesObj = JsonConvert.DeserializeObject(content);

                //TODO error handling!
                seriesObj = seriesObj.data;

                System.Console.WriteLine(seriesObj.season.GetType());

                return new Series
                {
                    Id = id,
                    SeriesName = seriesObj.seriesName,
                    Description = seriesObj.overview,
                    NumberOfSeasons = seriesObj.season,
                    Network = seriesObj.network,
                    Ended = seriesObj.status == "Ended",
                    FirstAired = seriesObj.firstAired,
                    AirsTime = seriesObj.airsTime,
                    Poster = seriesObj.poster,
                    Banner = seriesObj.banner,
                    Genres = seriesObj.genre.ToObject<List<string>>(),
                    Aliases = seriesObj.aliases.ToObject<List<string>>(),
                };
            }
            else
            {
                throw new HttpRequestException($"Error in GetSeriesByIdAsync, statuscode: {result.StatusCode}");
            }

        }
    }
}
