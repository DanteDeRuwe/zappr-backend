using System.Collections.Generic;

namespace Zappr.Api.Domain
{
    public class User
    {
        // Properties
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        // Nav Props
        public List<UserWatchListedSeries> WatchListedSeries { get; } = new List<UserWatchListedSeries>();
        public List<UserFavoriteSeries> FavoriteSeries { get; } = new List<UserFavoriteSeries>();
        public List<UserRatedSeries> RatedSeries { get; } = new List<UserRatedSeries>();
        public List<UserWatchedEpisode> WatchedEpisodes { get; } = new List<UserWatchedEpisode>();
        public List<UserRatedEpisode> RatedEpisodes { get; } = new List<UserRatedEpisode>();

        // Constructor
        public User() { }
        public User(int id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        // Methods
        public void AddSeriesToWatchList(Series series) => WatchListedSeries.Add(new UserWatchListedSeries { Series = series, SeriesId = series.Id, User = this, UserId = this.Id });
        public void AddFavoriteSeries(Series series) => FavoriteSeries.Add(new UserFavoriteSeries { Series = series, SeriesId = series.Id, User = this, UserId = this.Id });
        public void AddWatchedEpisode(Episode episode) => WatchedEpisodes.Add(new UserWatchedEpisode { Episode = episode, EpisodeId = episode.Id, User = this, UserId = this.Id });
        public void AddRatedEpisode(Episode episode) => RatedEpisodes.Add(new UserRatedEpisode { Episode = episode, EpisodeId = episode.Id, User = this, UserId = this.Id });
        public void AddRatedSeries(Series series) => RatedSeries.Add(new UserRatedSeries { Series = series, SeriesId = series.Id, User = this, UserId = this.Id });
    }
}
