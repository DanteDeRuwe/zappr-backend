using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zappr.Application.GraphQL.Interfaces;
using Zappr.Core.Entities;
using Zappr.Core.Interfaces;

namespace Zappr.Infrastructure.Data.Repositories
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Series> _series;
        private readonly ISeriesService _seriesService;

        public SeriesRepository(AppDbContext context, ISeriesService seriesService)
        {
            _context = context;
            _seriesService = seriesService;
            _series = context.Series;
        }

        public ICollection<Series> GetAll() => _series
            .Include(s => s.Comments).ThenInclude(c => c.Author)
            .Include(s => s.Ratings).ThenInclude(r => r.Author).ToList();

        public Series GetById(int id) => GetAll().SingleOrDefault(s => s.Id == id);

        public async Task<Series> GetByIdAsync(int id) => // Get series from db or API
            _series.Any(s => s.Id == id)
            ? GetAll().SingleOrDefault(s => s.Id == id)
            : await _seriesService.GetSeriesByIdAsync(id);

        public void Update(Series series) => _series.Update(series);
        public void SaveChanges() => _context.SaveChanges();
    }
}
