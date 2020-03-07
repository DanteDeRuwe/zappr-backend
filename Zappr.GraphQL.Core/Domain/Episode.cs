namespace Zappr.GraphQL.Core.Domain
{
    public class Episode : Watchable
    {
        public Episode(int id) : base(id) => Id = id;
    }
}
