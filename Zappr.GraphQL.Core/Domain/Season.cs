using System.Collections.Generic;

namespace Zappr.GraphQL.Core.Domain
{
    public class Season : Watchable
    {
        public ICollection<Episode> Episodes { get; } = new List<Episode>();
        public Season(int id) : base(id) => Id = id;
    }
}
