using System.Collections.Generic;

namespace Zappr.Api.Domain
{
    public class Series
    {
        // Properties
        public int Id { get; set; }
        public string SeriesName { get; set; }

        // Nullable properties
        public string? Poster { get; set; }
        public string? Description { get; set; }
        public int? NumberOfSeasons { get; set; }
        public string? Network { get; set; }
        public bool? Ended { get; set; }
        public string? FirstAired { get; set; }
        public string? AirsTime { get; set; }
        public string? Banner { get; set; }
        public List<string>? Genres { get; set; }
        public List<string>? Aliases { get; set; }

        // Nav Props
        public List<Episode> Episodes { get; } = new List<Episode>();
        public List<Character> Characters { get; } = new List<Character>();
        public List<Rating> Ratings { get; } = new List<Rating>();
        public List<Comment> Comments { get; } = new List<Comment>();

        // Constructors
        public Series() { }
        public Series(int id) => Id = id;
        public Series(int id, string name) { Id = id; SeriesName = name; }
        public Series(int id, string name, string poster) { Id = id; SeriesName = name; Poster = poster; }

        // Methods
        public void AddRating(Rating rating) => Ratings.Add(rating);
        public void AddComment(Comment comment) => Comments.Add(comment);

    }
}
