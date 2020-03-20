using System.Collections.Generic;

namespace Zappr.Api.Domain
{
    public class Episode
    {

        // Properties
        public int Id { get; set; }

        // Nav Props
        public List<Rating> Ratings { get; } = new List<Rating>();
        public List<Comment> Comments { get; } = new List<Comment>();


        // Methods
        public void AddRating(Rating rating) => Ratings.Add(rating);
        public void AddComment(Comment comment) => Comments.Add(comment);

        public Episode() { }

        public Episode(int id) => Id = id;
    }
}
