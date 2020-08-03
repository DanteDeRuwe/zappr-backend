using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Zappr.Core.Entities
{
    public class Series
    {
        // Properties
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Id will come from external API
        public int Id { get; set; }

        public string? Name { get; set; }
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

        //Calculated Properties
        public double AverageRating => Ratings.Any() ? Ratings.Average(r => r.Percentage) : 0.0;

        // Constructors
        public Series() { }

        // Methods
        public void AddRating(Rating rating) => Ratings.Add(rating);
        public void AddComment(Comment comment) => Comments.Add(comment);


        public override bool Equals(object obj) =>
            //Check for null and compare run-time types.
            (obj != null) && GetType() == obj.GetType() && Id == ((Series)obj).Id;

        public override int GetHashCode() => Id;
    }
}
