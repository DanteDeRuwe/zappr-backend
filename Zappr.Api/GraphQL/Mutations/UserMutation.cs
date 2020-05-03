using GraphQL.Types;
using System.Linq;
using Zappr.Api.Data;
using Zappr.Api.Domain;
using Zappr.Api.GraphQL.Types;
using Zappr.Api.Services;

namespace Zappr.Api.GraphQL.Mutations
{
    public class UserMutation : ObjectGraphType
    {
        private readonly IUserRepository _userRepository;
        private readonly TVMazeService _tvMaze;
        private readonly AppDbContext _dbContext;

        public UserMutation(IUserRepository userRepository, TVMazeService tvMaze, AppDbContext dbContext)
        {
            _userRepository = userRepository;
            _tvMaze = tvMaze;
            _dbContext = dbContext;
            Name = "Users";

            Field<UserType>(
              "createUser",
              arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<UserInputType>> { Name = "user" }
              ),
              resolve: context =>
              {
                  var user = context.GetArgument<User>("user");
                  var added = _userRepository.Add(user);
                  _userRepository.SaveChanges();
                  return added;
              });

            FieldAsync<UserType>(
                "addWatchedEpisode",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "userId" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "episodeId" }
                ),
                resolve: async context =>
                {
                    // Args
                    int episodeId = context.GetArgument<int>("episodeId");
                    int userId = context.GetArgument<int>("userId");

                    // Get user
                    var user = _userRepository.GetById(userId);

                    // Check if episodeid not already in watched
                    if (user.WatchedEpisodes.Any(fs => fs.EpisodeId == episodeId)) return user;

                    // Get Episode from db or API
                    var episode = _dbContext.Episodes.Any(e => e.Id == episodeId)
                        ? _dbContext.Episodes.Single(e => e.Id == episodeId)
                        : await _tvMaze.GetEpisodeByIdAsync(episodeId);

                    // Get its Series from db or API
                    episode.Series = _dbContext.Series.Any(s => s.Id == episode.SeriesId)
                            ? _dbContext.Series.SingleOrDefault(s => s.Id == episode.SeriesId)
                            : await _tvMaze.GetSeriesByIdAsync(episode.SeriesId);


                    // Add the episode to the users watched episodes and persist
                    user.AddWatchedEpisode(episode);
                    _userRepository.Update(user);
                    _userRepository.SaveChanges();
                    return user;
                }
            );

            Field<UserType>(
                "removeWatchedEpisode",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "userId" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "episodeId" }
                ),
                resolve: context =>
                {
                    var user = _userRepository.GetById(context.GetArgument<int>("userId"));
                    var episode = user.WatchedEpisodes.SingleOrDefault(we => we.EpisodeId == context.GetArgument<int>("episodeId"));

                    user.WatchedEpisodes.Remove(episode);

                    _userRepository.Update(user);
                    _userRepository.SaveChanges();

                    return user;
                }
            );


            FieldAsync<UserType>(
                "addFavoriteSeries",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "userId" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "seriesId" }
                ),
                resolve: async context =>
                {
                    // Args
                    int seriesId = context.GetArgument<int>("seriesId");
                    int userId = context.GetArgument<int>("userId");

                    // Get user
                    var user = _userRepository.GetById(userId);

                    // Check if seriesid not already in favorites
                    if (user.FavoriteSeries.Any(fs => fs.SeriesId == seriesId)) return user;

                    // Get series from db or API
                    var series = _dbContext.Series.Any(s => s.Id == seriesId)
                        ? _dbContext.Series.Single(s => s.Id == seriesId)
                        : await _tvMaze.GetSeriesByIdAsync(seriesId);

                    // Add the favorite and persist
                    user.AddFavoriteSeries(series);
                    _userRepository.Update(user);
                    _userRepository.SaveChanges();
                    return user;
                }
            );

        }
    }
}
