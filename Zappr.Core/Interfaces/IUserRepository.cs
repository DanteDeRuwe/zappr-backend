using Zappr.Core.Entities;

namespace Zappr.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public User Add(User user);
        public User FindByEmail(string email);
    }
}
