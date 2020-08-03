using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zappr.Core.Domain
{
    public class Episode
    {
        // Properties
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Summary { get; set; }
        public int? Season { get; set; }
        public int Number { get; set; }
        public string? AirDate { get; set; }
        public string? AirTime { get; set; }
        public int? Runtime { get; set; }
        public string? Image { get; set; }

        public int SeriesId { get; set; }

        // Nav Props
        public Series Series { get; set; }
        public List<Rating> Ratings { get; } = new List<Rating>();
        public List<Comment> Comments { get; } = new List<Comment>();

        // Methods
        public void AddRating(Rating rating) => Ratings.Add(rating);
        public void AddComment(Comment comment) => Comments.Add(comment);

        public Episode() { }

        public Episode(int id) => Id = id;
    }
}
