using System.Collections.Generic;

namespace Zappr.Api.Domain
{
    public class Series
    {
        // Properties
        public int Id { get; set; }
        public string Name { get; set; }

        // Nullable properties
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int? NumberOfSeasons { get; set; }
        public string? Network { get; set; }
        public bool? Ended { get; set; }
        public string? Premiered { get; set; }
        public string? AirTime { get; set; }
        public List<string>? Genres { get; set; }
        public string? OfficialSite { get; set; }

        // Nav Props
        public List<Episode> Episodes { get; } = new List<Episode>();
        public List<Character> Characters { get; } = new List<Character>();
        public List<Rating> Ratings { get; } = new List<Rating>();
        public List<Comment> Comments { get; } = new List<Comment>();

        // Constructors
        public Series() { }

        // Methods
        public void AddRating(Rating rating) => Ratings.Add(rating);
        public void AddComment(Comment comment) => Comments.Add(comment);

    }
}
