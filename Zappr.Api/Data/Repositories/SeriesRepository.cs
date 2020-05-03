using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zappr.Api.Domain;
using Zappr.Api.Services;

namespace Zappr.Api.Data.Repositories
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Series> _series;
        private readonly TVMazeService _tvMaze;

        public SeriesRepository(AppDbContext context, TVMazeService tvMaze)
        {
            _context = context;
            _series = context.Series;
            _tvMaze = tvMaze;
        }

        public List<Series> GetAll() => _series
            .Include(s => s.Comments).ThenInclude(c => c.Author)
            .Include(s => s.Ratings).ThenInclude(r => r.Author).ToList();

        public Series GetById(int id) => GetAll().SingleOrDefault(s => s.Id == id);

        public async Task<Series> GetByIdAsync(int id) => // Get series from db or API
            _series.Any(s => s.Id == id)
            ? GetAll().SingleOrDefault(s => s.Id == id)
            : await _tvMaze.GetSeriesByIdAsync(id);

        public void Update(Series series) => _series.Update(series);
        public void SaveChanges() => _context.SaveChanges();
        public void SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
