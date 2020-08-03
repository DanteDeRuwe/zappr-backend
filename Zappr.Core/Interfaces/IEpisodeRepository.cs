using System.Threading.Tasks;
using Zappr.Core.Entities;

namespace Zappr.Core.Interfaces
{
    public interface IEpisodeRepository : IRepository<Episode>
    {
        public Task<Episode> GetByIdAsync(int id);
    }
}
