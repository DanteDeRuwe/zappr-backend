using GraphQL.Types;

namespace Zappr.Api.GraphQL.Mutations
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
