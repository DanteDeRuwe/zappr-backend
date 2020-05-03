namespace Zappr.Api.Domain
{
    public abstract class UserEpisode
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int EpisodeId { get; set; }
        public Episode Episode { get; set; }

        public override bool Equals(object other) =>
            other?.GetType() == GetType()
            && UserId == ((UserEpisode)other).UserId
            && EpisodeId == ((UserEpisode)other).EpisodeId;

        public override int GetHashCode() => UserId.GetHashCode() ^ EpisodeId.GetHashCode(); //https://stackoverflow.com/a/70375/12641623
    }

}