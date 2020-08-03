namespace Zappr.Core.Domain
{
    public class Rating
    {
        // Props
        public int Id { get; set; }
        public int Percentage { get; set; }
        public User Author { get; private set; }

        // Constructor
        public Rating() { }
        public Rating(int percentage, User author)
        {
            Percentage = percentage;
            Author = author;
        }

    }
}