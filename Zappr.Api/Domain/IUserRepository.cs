using System.Collections.Generic;

namespace Zappr.Api.Domain
{
    public interface IUserRepository
    {
        public User Add(User user);
        public User Delete(User user);
        public List<User> GetAll();
        public User GetById(int id);
        public void Update(User user);
        public void SaveChanges();
        public void SaveChangesAsync();
    }
}
