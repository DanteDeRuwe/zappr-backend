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

            //Single Search by name
            Field<SeriesType>(
                "singlesearch",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "name" }),
                resolve: context => _tvmaze.SingleSearchByNameAsync(context.GetArgument<string>("name"))
            );


            //Scheduled shows per country
            Field<ListGraphType<SeriesType>>(
                "today",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "country" }),
                resolve: context => _tvmaze.GetScheduleAsync(context.GetArgument<string>("country"))
            );

            //Scheduled shows per country for the whole week
            Field<ListGraphType<SeriesType>>(
                "schedule",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "country" },
                    new QueryArgument<IntGraphType>
                    { Name = "start", Description = "the number of days from today you want to take as the start" },
                    new QueryArgument<IntGraphType>
                    { Name = "numberofdays", Description = "the number of days you want to include in the schedule" }
                ),
                resolve: context => _tvmaze.GetScheduleMultipleDaysFromTodayAsync(
                    context.GetArgument<string>("country"),
                    context.GetArgument<int>("start"),
                    context.GetArgument<int>("numberofdays")
                )
            );
        }
    }
}
