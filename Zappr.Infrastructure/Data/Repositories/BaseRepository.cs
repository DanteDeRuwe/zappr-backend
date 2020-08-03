using System.Collections.Generic;
using Zappr.Core.Interfaces;

namespace Zappr.Infrastructure.Data.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        private readonly AppDbContext _context;

        protected BaseRepository(AppDbContext context) => _context = context;

        public void SaveChanges() => _context.SaveChanges();
        public void Update(T item) => _context.Update(item);

        public abstract ICollection<T> GetAll();
        public abstract T GetById(int id);
    }
}
