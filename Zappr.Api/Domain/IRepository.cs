using System.Collections.Generic;

namespace Zappr.Api.Domain
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
    }
}
