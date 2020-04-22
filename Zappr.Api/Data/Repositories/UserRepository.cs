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

        public List<User> GetAll() => _users.ToList(); //just user data

        // When getting by id, include all series and episode data
        public User GetById(int id) => _users.Include(u => u.FavoriteSeries).ThenInclude(us => us.Series)
                                            .Include(u => u.WatchListedSeries).ThenInclude(us => us.Series)
                                            .Include(u => u.RatedSeries).ThenInclude(us => us.Series)
                                            .Include(u => u.WatchedEpisodes).ThenInclude(ue => ue.Episode)
                                            .Include(u => u.RatedEpisodes).ThenInclude(ue => ue.Episode)
                                            .SingleOrDefault(u => u.Id == id);

        public User Add(User user)
        {
            _users.Add(user);
            return user;
        }

        public User Delete(User item) => throw new System.NotImplementedException(); //TODO
        public void SaveChanges() => _context.SaveChanges();
    }
}