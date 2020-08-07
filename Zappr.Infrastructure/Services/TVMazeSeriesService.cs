using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Zappr.Application.GraphQL.Interfaces;
using Zappr.Core.Entities;

namespace Zappr.Infrastructure.Services
{
    public class TvMazeSeriesService : ApiService, ISeriesService
    {
        public TvMazeSeriesService(IConfiguration configuration, HttpClient client) : base(configuration, client) { }

        public async Task<Series> GetSeriesByIdAsync(int id)
        {
            string url = QueryHelpers.AddQueryString($"shows/{id}", "embed", "seasons");
            var result = GetHttpResponse(url);

            if (result.IsSuccessStatusCode)
            {
                string content = await result.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(content);

                return ConstructSeries(data);
            }
            else
            {
                //TODO
                throw new HttpRequestException($"Error in GetSeriesByIdAsync, statuscode: {result.StatusCode}");
            }
        }

        public async Task<List<Series>> SearchSeriesByNameAsync(string name)
        {
            string url = QueryHelpers.AddQueryString(
                "search/shows",
                new Dictionary<string, string>() { { "q", name }, { "embed", "seasons" } }
            );

            var result = GetHttpResponse(url);


            if (result.IsSuccessStatusCode)
            {
                string content = await result.Content.ReadAsStringAsync();
                dynamic resObj = JsonConvert.DeserializeObject(content);

                //TODO error handling!
                JArray seriesArr = resObj;
                var list = seriesArr.ToObject<List<dynamic>>().ToList();

                return list.Select(s => ConstructSeries(s.show) as Series).ToHashSet().ToList();

            }
            else
            {
                //TODO
                throw new HttpRequestException($"Error in SearchSeriesByNameAsync, statuscode: {result.StatusCode}");
            }
        }

        public async Task<Series> SingleSearchSeriesByNameAsync(string name)
        {
            string url = QueryHelpers.AddQueryString("singlesearch/shows", "q", name);
            var result = GetHttpResponse(url);


            if (result.IsSuccessStatusCode)
            {
                string content = await result.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(content);

                return ConstructSeries(data);
            }
            else
            {
                //TODO
                throw new HttpRequestException($"Error in SingleSearchSeriesByNameAsync, statuscode: {result.StatusCode}");
            }
        }

        public async Task<List<Series>> GetScheduleAsync(string countrycode, string date = null)
        {

            date ??= DateTime.Now.ToString("yyyy-MM-dd");

            string url = QueryHelpers.AddQueryString(
                "schedule",
                new Dictionary<string, string>() { { "country", countrycode }, { "date", date } }
            );

            var result = GetHttpResponse(url);


            if (result.IsSuccessStatusCode)
            {
                string content = await result.Content.ReadAsStringAsync();
                dynamic resObj = JsonConvert.DeserializeObject(content);

                //TODO error handling!
                JArray seriesArr = resObj;
                var list = seriesArr.ToObject<List<dynamic>>().ToList();

                return list.Select(s => ConstructSeries(s.show) as Series).ToHashSet().ToList();

            }
            else
            {
                //TODO
                throw new HttpRequestException($"Error in SearchSeriesByNameAsync, statuscode: {result.StatusCode}");
            }
        }

        public async Task<List<Series>> GetScheduleMultipleDaysFromTodayAsync(string country, int startFromToday = 0, int days = 7)
        {

            var schedule = new List<Series>();
            for (int i = startFromToday; i < startFromToday + days; i++)
            {
                var thisday = await GetScheduleAsync(country, DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
                schedule = schedule.Concat(thisday).ToHashSet().ToList(); //toHashSet to eliminate duplicates
            }

            return schedule;
        }


        private Series ConstructSeries(dynamic seriesObj) => new Series
        {
            Id = seriesObj.id,
            Name = seriesObj.name,
            Description = seriesObj.summary,
            Network = seriesObj.network?.ToObject<dynamic>()?.name,
            Ended = seriesObj.status == "Ended",
            Premiered = seriesObj.premiered,
            ImageUrl = seriesObj.image?.ToObject<dynamic>()?.medium,
            Genres = seriesObj.genres?.ToObject<List<string>>(),
            AirTime = seriesObj.schedule?.ToObject<dynamic>()?.time,
            OfficialSite = seriesObj.officialSite,
            NumberOfSeasons = seriesObj._embedded?.ToObject<dynamic>().seasons.Count
        };

    }
}
