using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zappr.Api.Data;
using Zappr.Core.Domain;
using Zappr.Infrastructure.Services;

namespace Zappr.Infrastructure.Data.Repositories
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Episode> _episodes;
        private readonly TVMazeService _tvMaze;

        public EpisodeRepository(AppDbContext context, TVMazeService tvMaze)
        {
            _context = context;
            _episodes = context.Episodes;
            _tvMaze = tvMaze;
        }

        public List<Episode> GetAll() => _episodes
            .Include(e => e.Comments).ThenInclude(e => e.Author)
            .Include(e => e.Ratings).ThenInclude(e => e.Author)
            .ToList();

        public Episode GetById(int id) => GetAll().SingleOrDefault(e => e.Id == id);

        public async Task<Episode> GetByIdAsync(int id) => // Get episodes from db or API
            _episodes.Any(e => e.Id == id)
                ? GetAll().SingleOrDefault(e => e.Id == id)
                : await _tvMaze.GetEpisodeByIdAsync(id);

        public void Update(Episode episode) => _episodes.Update(episode);
        public void SaveChanges() => _context.SaveChanges();
        public void SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
