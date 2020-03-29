using GraphQL.Types;
using Zappr.Api.GraphQL.Types;
using Zappr.Api.Services;

namespace Zappr.Api.GraphQL
{
    public class SeriesQuery : ObjectGraphType
    {
        readonly TVMazeService _tvmaze;

        public SeriesQuery(TVMazeService tvMazeService)
        {
            _tvmaze = tvMazeService;
            Name = "Series";

            // get by id
            Field<SeriesType>(
                "get",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => _tvmaze.GetSeriesByIdAsync(context.GetArgument<int>("id"))
            );

            //Search by name
            Field<ListGraphType<SeriesType>>(
                "search",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "name" }),
                resolve: context => _tvmaze.SearchByNameAsync(context.GetArgument<string>("name"))
            );
        }
    }
}
