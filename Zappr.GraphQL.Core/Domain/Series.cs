using System.Collections.Generic;

namespace Zappr.GraphQL.Core.Domain
{
    public class Series : Watchable
    {
        // Nav Props
        public ICollection<Season> Seasons = new List<Season>();

        // Constructor
        public Series(int id) : base(id) => Id = id;

    }
}
