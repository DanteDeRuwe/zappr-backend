using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zappr.Core.Domain
{
    public interface IEpisodeRepository
    {
        public List<Episode> GetAll();
        public Episode GetById(int id);
        public Task<Episode> GetByIdAsync(int id);
        public void Update(Episode episode);
        public void SaveChanges();
        public void SaveChangesAsync();
    }
}
