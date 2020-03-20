using GraphQL.Types;
using Zappr.Api.GraphQL.Types;
using Zappr.Api.Services;

namespace Zappr.Api.GraphQL
{
    public class SeriesQuery : ObjectGraphType
    {
        readonly TvdbService _tvdb;

        public SeriesQuery(TvdbService tvdbService)
        {

            _tvdb = tvdbService;
            Name = "Series";

            // get by id
            QueryArguments args = new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" });
            Field<SeriesType>(
                "get",
                arguments: args,
                resolve: context => _tvdb.GetSeriesByIdAsync(context.GetArgument<int>("id"))
            );

        }
    }
}
