using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Zappr.Core.Entities;
using Zappr.Core.Interfaces;

namespace Zappr.Infrastructure.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly DbSet<User> _users;

        public UserRepository(AppDbContext context) : base(context) => _users = context.Users;

        public override ICollection<User> GetAll() => _users
            .Include(u => u.FavoriteSeries).ThenInclude(us => us.Series)
            .Include(u => u.WatchListedSeries).ThenInclude(us => us.Series)
            .Include(u => u.RatedSeries).ThenInclude(us => us.Series).ThenInclude(s => s.Ratings)
            .Include(u => u.WatchedEpisodes).ThenInclude(ue => ue.Episode).ThenInclude(e => e.Series)
            .Include(u => u.RatedEpisodes).ThenInclude(ue => ue.Episode).ThenInclude(e => e.Series)
            .Include(u => u.RatedEpisodes).ThenInclude(ue => ue.Episode.Ratings)
            .ToList();

        // When getting by id, include all series and episode data
        public override User GetById(int id) => GetAll().SingleOrDefault(u => u.Id == id);
        public User FindByEmail(string email) => GetAll().SingleOrDefault(u => u.Email == email);

        public User Add(User user)
        {
            _users.Add(user);
            return user;
        }
    }
}