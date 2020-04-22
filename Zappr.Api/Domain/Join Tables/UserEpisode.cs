namespace Zappr.Api.Domain
{
    public abstract class UserEpisode
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int EpisodeId { get; set; }
        public Episode Episode { get; set; }
    }
}