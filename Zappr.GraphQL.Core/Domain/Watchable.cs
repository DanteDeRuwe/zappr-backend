using System.Collections.Generic;

namespace Zappr.GraphQL.Core.Domain
{
    public abstract class Watchable
    {
        // Properties
        public int Id { get; set; }

        // Nav Props
        public ICollection<Rating> Ratings { get; } = new List<Rating>();
        public ICollection<Comment> Comments { get; } = new List<Comment>();

        // Constructor
        protected Watchable(int id) => Id = id;

        // Methods
        public void AddRating(Rating rating) => Ratings.Add(rating);
        public void AddComment(Comment comment) => Comments.Add(comment);

    }
}
