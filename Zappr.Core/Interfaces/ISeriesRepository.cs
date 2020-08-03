using System.Threading.Tasks;
using Zappr.Core.Entities;

namespace Zappr.Core.Interfaces
{
    public interface ISeriesRepository : IRepository<Series>
    {
        public Task<Series> GetByIdAsync(int id);
    }
}
