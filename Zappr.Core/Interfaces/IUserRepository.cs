using System.Collections.Generic;
using Zappr.Core.Entities;

namespace Zappr.Core.Interfaces
{
    public interface IUserRepository
    {
        public User Add(User user);
        public User Delete(User user);
        public List<User> GetAll();
        public User GetById(int id);
        public User FindByEmail(string email);
        public void Update(User user);
        public void SaveChanges();
        public void SaveChangesAsync();
    }
}
