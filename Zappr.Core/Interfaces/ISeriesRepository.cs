using System.Collections.Generic;
using System.Threading.Tasks;
using Zappr.Core.Entities;

namespace Zappr.Core.Interfaces
{
    public interface ISeriesRepository
    {
        public List<Series> GetAll();
        public Series GetById(int id);
        public Task<Series> GetByIdAsync(int id);
        public void Update(Series series);
        public void SaveChanges();
        public void SaveChangesAsync();
    }
}
