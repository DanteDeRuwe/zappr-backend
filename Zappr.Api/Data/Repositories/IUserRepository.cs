using System.Collections.Generic;
using Zappr.Api.Domain;

namespace Zappr.Api.Data.Repositories
{
    public interface IUserRepository
    {
        ICollection<User> GetAll();
        User GetUserById(int id);
    }
}
