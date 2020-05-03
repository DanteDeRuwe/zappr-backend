using GraphQL.Types;
using Zappr.Api.GraphQL.Mutations;

namespace Zappr.Api.GraphQL
{
    public class ZapprMutation : ObjectGraphType
    {
        public ZapprMutation()
        {
            Name = "Mutation";

            Field<UserMutation>("users", resolve: context => new { });
            Field<SeriesMutation>("series", resolve: context => new { });
        }
    }
}
