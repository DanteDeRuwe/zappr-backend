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
        public ISet<UserWatchListedSeries> WatchListedSeries { get; } = new HashSet<UserWatchListedSeries>();
        public ISet<UserFavoriteSeries> FavoriteSeries { get; } = new HashSet<UserFavoriteSeries>();
        public ISet<UserRatedSeries> RatedSeries { get; } = new HashSet<UserRatedSeries>();
        public ISet<UserWatchedEpisode> WatchedEpisodes { get; } = new HashSet<UserWatchedEpisode>();
        public ISet<UserRatedEpisode> RatedEpisodes { get; } = new HashSet<UserRatedEpisode>();

        // Constructor
        public User() { }
        public User(int id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        // Methods
        public void AddSeriesToWatchList(Series series) => WatchListedSeries.Add(new UserWatchListedSeries { Series = series, SeriesId = series.Id, User = this, UserId = Id });
        public void AddFavoriteSeries(Series series) => FavoriteSeries.Add(new UserFavoriteSeries { Series = series, SeriesId = series.Id, User = this, UserId = Id });
        public void AddWatchedEpisode(Episode episode) => WatchedEpisodes.Add(new UserWatchedEpisode { Episode = episode, EpisodeId = episode.Id, User = this, UserId = Id });
        public void AddRatedEpisode(Episode episode) => RatedEpisodes.Add(new UserRatedEpisode { Episode = episode, EpisodeId = episode.Id, User = this, UserId = Id });
        public void AddRatedSeries(Series series) => RatedSeries.Add(new UserRatedSeries { Series = series, SeriesId = series.Id, User = this, UserId = Id });
    }
}
