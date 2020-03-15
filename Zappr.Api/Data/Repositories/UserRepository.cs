using System.Collections.Generic;
using System.Linq;
using Zappr.Api.Domain;

namespace Zappr.Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ICollection<User> _tempUsers = new List<User>
        {
            new User(0, "John"), new User(1, "Jane"), new User(2, "Joan"), new User(3, "June"), new User(4, "Jean")
        };

        public ICollection<User> GetAll() => _tempUsers;
        public User GetUserById(int id) => _tempUsers.FirstOrDefault(u => u.Id == id);
    }
}
