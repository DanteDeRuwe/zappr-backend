using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Zappr.Api.Domain;

namespace Zappr.Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(AppDbContext context)
        {
            _context = context;
            _users = context.Users;
        }

        public List<User> GetAll() => _users
            .Include(u => u.FavoriteSeries).ThenInclude(us => us.Series)
            .Include(u => u.WatchListedSeries).ThenInclude(us => us.Series)
            .Include(u => u.RatedSeries).ThenInclude(us => us.Series).ThenInclude(s => s.Ratings)
            .Include(u => u.WatchedEpisodes).ThenInclude(ue => ue.Episode).ThenInclude(e => e.Series)
            .Include(u => u.RatedEpisodes).ThenInclude(ue => ue.Episode).ThenInclude(e => e.Series)
            .Include(u => u.RatedEpisodes).ThenInclude(ue => ue.Episode.Ratings)
            .ToList();

        // When getting by id, include all series and episode data
        public User GetById(int id) => GetAll().SingleOrDefault(u => u.Id == id);
        public User FindByEmail(string email) => GetAll().SingleOrDefault(u => u.Email == email);

        public User Add(User user)
        {
            _users.Add(user);
            return user;
        }

        public User Delete(User user) => throw new System.NotImplementedException(); //TODO
        public void Update(User user) => _context.Update(user);
        public void SaveChanges() => _context.SaveChanges();
        public void SaveChangesAsync() => _context.SaveChangesAsync();
    }
}