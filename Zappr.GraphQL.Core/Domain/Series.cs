using System.Collections.Generic;

namespace Zappr.GraphQL.Core.Domain
{
    public class Series : Watchable
    {
        // Nav Props
        public ICollection<Season> Seasons { get; } = new List<Season>();
        public ICollection<Actor> Actors { get; } = new List<Actor>();

        // Constructor
        public Series(int id) : base(id) => Id = id;

    }
}
