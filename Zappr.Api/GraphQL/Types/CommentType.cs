using GraphQL.Types;
using Zappr.Core.Domain;

namespace Zappr.Api.GraphQL.Types
{
    public class CommentType : ObjectGraphType<Comment>
    {
        public CommentType()
        {
            Field(c => c.Id);
            Field(c => c.Text);

            Field<UserType>("author", resolve: c => c.Source.Author);
        }
    }
}