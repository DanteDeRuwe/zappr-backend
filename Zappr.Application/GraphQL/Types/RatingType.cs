using GraphQL.Types;
using Zappr.Core.Entities;

namespace Zappr.Api.GraphQL.Types
{
    public class RatingType : ObjectGraphType<Rating>
    {
        public RatingType()
        {
            Field(r => r.Id);
            Field(r => r.Percentage);

            Field<UserType>("author", resolve: c => c.Source.Author);
        }
    }
}