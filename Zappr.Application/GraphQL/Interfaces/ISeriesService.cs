using System.Collections.Generic;
using System.Threading.Tasks;
using Zappr.Core.Entities;

namespace Zappr.Application.GraphQL.Interfaces
{
    public interface ISeriesService
    {
        public Task<Series> GetSeriesByIdAsync(int id);
        public Task<List<Series>> SearchSeriesByNameAsync(string name);
        public Task<Series> SingleSearchSeriesByNameAsync(string name);
        public Task<List<Series>> GetScheduleAsync(string countrycode, string date = null);
        public Task<List<Series>> GetScheduleMultipleDaysFromTodayAsync(string country, int startFromToday = 0, int days = 7);
    }
}
