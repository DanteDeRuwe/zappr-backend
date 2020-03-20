using GraphQL.Types;

namespace Zappr.Api.GraphQL
{
    public class ZapprQuery : ObjectGraphType
    {
        public ZapprQuery()
        {
            Name = "Query";

            Field<SeriesQuery>("series", resolve: context => new { });
            Field<UserQuery>("users", resolve: context => new { });
        }

    }
}
