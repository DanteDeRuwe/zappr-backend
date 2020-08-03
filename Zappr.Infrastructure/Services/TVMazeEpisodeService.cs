using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Zappr.Application.GraphQL.Interfaces;
using Zappr.Core.Entities;

namespace Zappr.Infrastructure.Services
{
    public class TvMazeEpisodeService : APIService, IEpisodeService
    {
        public TvMazeEpisodeService(IConfiguration configuration) => _configuration = configuration;

        public async Task<Episode> GetEpisodeByIdAsync(int id)
        {
            string baseUrl = "http://api.tvmaze.com/episodes/" + id;
            string url = buildUrlWithQueries(baseUrl,
                new Dictionary<string, string>() { { "embed", "show" } });
            var result = GetHttpResponse(url);


            if (result.IsSuccessStatusCode)
            {
                string content = await result.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(content);

                return ConstructEpisode(data);
            }
            else
            {
                //TODO
                throw new HttpRequestException($"Error in GetEpisodeByIdAsync, statuscode: {result.StatusCode}");
            }
        }

        private Episode ConstructEpisode(dynamic data) => new Episode
        {
            Id = data.id,
            Name = data.name,
            Summary = data.summary,
            Season = data.season,
            Number = data.number,
            AirDate = data.airdate,
            AirTime = data.airtime,
            Runtime = data.runtime,
            Image = data.image?.ToObject<dynamic>()?.medium,
            SeriesId = data._embedded?.ToObject<dynamic>()?.show?.ToObject<dynamic>()?.id
        };

    }
}