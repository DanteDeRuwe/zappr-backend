using System.Collections.Generic;

namespace Zappr.Api.Domain
{
    public class User
    {
        // Properties
        public int Id { get; private set; }
        public string Email { get; private set; }
        public string FullName { get; set; }


        // Nav Props
        public List<UserSeries> WatchList { get; } = new List<UserSeries>();
        public List<UserSeries> FavoriteSeries { get; } = new List<UserSeries>();
        public List<UserEpisode> WatchedEpisodes { get; } = new List<UserEpisode>();
        public List<UserSeries> RatedSeries { get; } = new List<UserSeries>();
        public List<UserEpisode> RatedEpisodes { get; } = new List<UserEpisode>();

        // Constructor
        public User() { }
        public User(int id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        // Methods
        public void AddSeriesToWatchList(Series series) => WatchList.Add(new UserSeries { Series = series, SeriesId = series.Id, User = this, UserId = this.Id });
        public void AddFavoriteSeries(Series series) => FavoriteSeries.Add(new UserSeries { Series = series, SeriesId = series.Id, User = this, UserId = this.Id });
        public void AddWatchedEpisode(Episode episode) => WatchedEpisodes.Add(new UserEpisode { Episode = episode, EpisodeId = episode.Id, User = this, UserId = this.Id });
        public void AddRatedEpisode(Episode episode) => RatedEpisodes.Add(new UserEpisode { Episode = episode, EpisodeId = episode.Id, User = this, UserId = this.Id });
        public void AddRatedSeries(Series series) => RatedSeries.Add(new UserSeries { Series = series, SeriesId = series.Id, User = this, UserId = this.Id });
    }
}
