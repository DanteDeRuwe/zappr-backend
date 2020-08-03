using System.Threading.Tasks;
using Zappr.Core.Entities;

namespace Zappr.Application.GraphQL.Interfaces
{
    public interface IEpisodeService
    {
        public Task<Episode> GetEpisodeByIdAsync(int id);
    }
}
