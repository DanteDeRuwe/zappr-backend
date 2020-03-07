using System.Collections.Generic;

namespace Zappr.GraphQL.Core.Domain
{
    public class User
    {
        // Properties
        public int Id { get; private set; }
        public string FullName { get; private set; }


        // Nav Props
        public ICollection<Series> WatchList { get; } = new List<Series>();
        public ICollection<Series> FavoriteSeries { get; } = new List<Series>();
        public ICollection<Episode> WatchedEpisodes { get; } = new List<Episode>();
        public ICollection<Watchable> RatedWatchables { get; } = new List<Watchable>();


        // Constructor
        public User(int id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        // Methods
        public void AddSeriesToWatchList(Series series) => WatchList.Add(series);
        public void AddFavoriteSeries(Series series) => FavoriteSeries.Add(series);
        public void AddWatchedEpisode(Episode episode) => WatchedEpisodes.Add(episode);
        public void AddRatedWatchable(Watchable watchable) => RatedWatchables.Add(watchable);

    }
}
