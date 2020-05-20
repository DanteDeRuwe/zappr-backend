using GraphQL.Authorization;
using GraphQL.Types;
using System.Linq;
using Zappr.Api.Domain;
using Zappr.Api.GraphQL.Types;
using Zappr.Api.Services;

namespace Zappr.Api.GraphQL.Mutations
{
    public class SeriesMutation : ObjectGraphType
    {
        private readonly IUserRepository _userRepository;
        private readonly TVMazeService _tvMaze;
        private readonly ISeriesRepository _seriesRepository;

        public SeriesMutation(IUserRepository userRepository, TVMazeService tvMaze, ISeriesRepository seriesRepository)
        {
            _userRepository = userRepository;
            _tvMaze = tvMaze;
            _seriesRepository = seriesRepository;
            Name = "SeriesMutation";

            //Auth
            this.AuthorizeWith("UserPolicy");

            FieldAsync<SeriesType>(
                "addComment",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "seriesId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "commentText" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "authorUserId" }
                ),
                resolve: async context =>
                {
                    //Args
                    string commentText = context.GetArgument<string>("commentText");
                    int authorId = context.GetArgument<int>("authorUserId");
                    int seriesId = context.GetArgument<int>("seriesId");

                    //Get the author
                    var author = _userRepository.GetById(authorId);

                    // Get series from db or API
                    var series = await _seriesRepository.GetByIdAsync(seriesId);

                    // Add the comment
                    Comment comment = new Comment(commentText, author);
                    series.AddComment(comment);

                    _seriesRepository.Update(series);
                    _seriesRepository.SaveChanges();

                    return series;

                });

            FieldAsync<SeriesType>(
                "addRating",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "seriesId" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "ratingPercentage" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "authorUserId" }
                ),
                resolve: async context =>
                {
                    //Args
                    int percentage = context.GetArgument<int>("ratingPercentage");
                    int authorId = context.GetArgument<int>("authorUserId");
                    int seriesId = context.GetArgument<int>("seriesId");

                    //Get the author
                    var author = _userRepository.GetById(authorId);

                    // Get series from db or API
                    var series = await _seriesRepository.GetByIdAsync(seriesId);

                    // Update rating if user has already rated, or add a new one
                    var ratingByUser = series.Ratings.SingleOrDefault(r => r.Author == author);
                    if (ratingByUser != null)
                        ratingByUser.Percentage = percentage;
                    else
                        series.AddRating(new Rating(percentage, author));

                    _seriesRepository.Update(series);
                    _seriesRepository.SaveChanges();

                    //Add the series to the rated series of the author
                    author.AddRatedSeries(series);
                    _userRepository.Update(author);
                    _userRepository.SaveChanges();


                    return series;

                });
        }
    }
}
