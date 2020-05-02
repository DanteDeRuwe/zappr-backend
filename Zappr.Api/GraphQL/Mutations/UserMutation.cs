using GraphQL.Types;
using System.Linq;
using Zappr.Api.Domain;
using Zappr.Api.GraphQL.Types;
using Zappr.Api.Services;

namespace Zappr.Api.GraphQL.Mutations
{
    public class UserMutation : ObjectGraphType
    {
        private readonly IUserRepository _userRepository;
        private readonly TVMazeService _tvMaze;

        public UserMutation(IUserRepository userRepository, TVMazeService tvMaze)
        {
            _userRepository = userRepository;
            _tvMaze = tvMaze;
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
                    //TODO check if episode in db

                    //Get Episode from API
                    var episode = await _tvMaze.GetEpisodeByIdAsync(context.GetArgument<int>("episodeId"));
                    var series = await _tvMaze.GetSeriesByIdAsync(episode.SeriesId);
                    episode.Series = series;

                    var user = _userRepository.GetById(context.GetArgument<int>("userId"));

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

        }
    }
}
