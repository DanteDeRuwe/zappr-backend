using GraphQL.Types;
using Zappr.Api.GraphQL.Mutations;

namespace Zappr.Api.GraphQL
{
    public class ZapprMutation : ObjectGraphType
    {
        public ZapprMutation()
        {
            Name = "Mutation";

            Field<UserMutation>("userMutation", resolve: context => new { });
            Field<SeriesMutation>("seriesMutation", resolve: context => new { });
        }
    }
}
