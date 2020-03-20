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
            Field<SeriesType>(
                "get",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
        resolve: context => _tvdb.GetSeriesByIdAsync(context.GetArgument<int>("id"))
            );

            //Search by name
            Field<ListGraphType<SeriesType>>(
                "search",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "name" }),
                resolve: context => _tvdb.SearchSeriesByNameAsync(context.GetArgument<string>("name"))
            );
        }
    }
}
