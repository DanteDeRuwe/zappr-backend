using System.Collections.Generic;

namespace Zappr.GraphQL.Core.Domain
{
    public class Comment
    {
        // Properties
        public string Text { get; set; }

        // Nav Props
        public User Author { get; private set; }
        public ICollection<Comment> Replies = new List<Comment>();

        // Constructor
        public Comment(string text, User author)
        {
            Text = text;
            Author = author;
        }
    }
}