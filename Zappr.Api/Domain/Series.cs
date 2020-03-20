using System.Collections.Generic;

namespace Zappr.Api.Domain
{
    public class Series
    {
        // Properties
        public int Id { get; set; }

        // Nav Props
        public List<Episode> Episodes { get; } = new List<Episode>();
        public List<Character> Characters { get; } = new List<Character>();
        public List<Rating> Ratings { get; } = new List<Rating>();
        public List<Comment> Comments { get; } = new List<Comment>();

        // Constructor
        public Series() { }
        public Series(int id) => Id = id;

        // Methods
        public void AddRating(Rating rating) => Ratings.Add(rating);
        public void AddComment(Comment comment) => Comments.Add(comment);

    }
}
