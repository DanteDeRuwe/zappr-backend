using System.Collections.Generic;

namespace Zappr.Api.Domain
{
    public class Episode
    {

        // Properties
        public int Id { get; set; }
        public int SeasonNumber { get; set; }

        // Nav Props
        public ICollection<Rating> Ratings { get; } = new List<Rating>();
        public ICollection<Comment> Comments { get; } = new List<Comment>();

        // Methods
        public void AddRating(Rating rating) => Ratings.Add(rating);
        public void AddComment(Comment comment) => Comments.Add(comment);

        public Episode() { }

        public Episode(int id, int seasonNumber)
        {
            SeasonNumber = seasonNumber;
            Id = id;
        }

        public Episode(int id) : this(id, 1) { }
    }
}
