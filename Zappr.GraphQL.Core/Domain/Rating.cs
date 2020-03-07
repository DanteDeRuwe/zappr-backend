namespace Zappr.GraphQL.Core.Domain
{
    public class Rating
    {
        // Props
        public int Percentage { get; set; }
        public User Author { get; private set; }

        // Constructor
        public Rating(int percentage, User author)
        {
            Percentage = percentage;
            Author = author;
        }

    }
}