using System.Collections.Generic;

namespace Zappr.Core.Domain
{
    public class Comment
    {
        // Properties
        public int Id { get; set; }
        public string Text { get; set; }

        // Nav Props
        public User Author { get; set; }
        public List<Comment> Replies = new List<Comment>();

        // Constructors
        public Comment() { }

        public Comment(string text, User author)
        {
            Text = text;
            Author = author;
        }
    }
}