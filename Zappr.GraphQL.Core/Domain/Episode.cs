namespace Zappr.GraphQL.Core.Domain
{
    public class Episode : Watchable
    {
        public int SeasonNumber { get; set; }

        public Episode(int id, int seasonNumber) : base(id)
        {
            SeasonNumber = seasonNumber;
            Id = id;
        }

        //when no season specified, just call it season 1
        public Episode(int id) : this(id, 1) { }
    }
}
