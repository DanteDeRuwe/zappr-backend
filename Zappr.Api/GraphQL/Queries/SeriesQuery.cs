using GraphQL.Types;
using Zappr.Api.GraphQL.Types;
using Zappr.Core.Interfaces;
using Zappr.Infrastructure.Services;

namespace Zappr.Api.GraphQL
{
    public class SeriesQuery : ObjectGraphType
    {
        private readonly ISeriesRepository _seriesRepository;
        private readonly TVMazeService _tvmaze;

        public SeriesQuery(ISeriesRepository seriesRepository, TVMazeService tvMaze)
        {
            _seriesRepository = seriesRepository;
            _tvmaze = tvMaze;
            Name = "SeriesQuery";

            // get by id
            Field<SeriesType>(
                "get",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => _seriesRepository.GetByIdAsync(context.GetArgument<int>("id"))
            );

            //Search by name (note, this only pulls from external API: less data)
            Field<ListGraphType<SeriesType>>(
                "search",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "name" }),
                resolve: context => _tvmaze.SearchSeriesByNameAsync(context.GetArgument<string>("name"))
            );

            FieldAsync<SeriesType>(
            "singlesearch",
            arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "name" }),
            resolve: async context =>
            {
                var res = await _tvmaze.SingleSearchSeriesByNameAsync(context.GetArgument<string>("name"));
                //Look up in db if possible for full results
                return await _seriesRepository.GetByIdAsync(res.Id);
            });


            //Scheduled shows per country (note, this only pulls from external API: less data)
            Field<ListGraphType<SeriesType>>(
                "today",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "country" }),
                resolve: context => _tvmaze.GetScheduleAsync(context.GetArgument<string>("country"))
            );

            //Scheduled shows per country for the whole week (note, this only pulls from external API: less data)
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
