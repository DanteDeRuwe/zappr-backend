using System.Collections.Generic;

namespace Zappr.Core.Interfaces
{
    public interface IRepository<T>
    {
        public ICollection<T> GetAll();
        public T GetById(int id);
        public void Update(T item);
        public void SaveChanges();
    }
}
