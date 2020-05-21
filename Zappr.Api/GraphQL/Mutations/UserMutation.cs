using GraphQL;
using GraphQL.Authorization;
using GraphQL.Types;
using System;
using System.Linq;
using Zappr.Api.Domain;
using Zappr.Api.GraphQL.Types;
using Zappr.Api.Helpers;
using Zappr.Api.Services;
using BCr = BCrypt.Net.BCrypt;

namespace Zappr.Api.GraphQL.Mutations
{
    public class UserMutation : ObjectGraphType
    {
        private readonly IUserRepository _userRepository;
        private readonly TVMazeService _tvMaze;
        private readonly ISeriesRepository _seriesRepository;
        private readonly IEpisodeRepository _episodeRepository;
        private readonly TokenHelper _tokenHelper;

        public UserMutation(IUserRepository userRepository, TVMazeService tvMaze, ISeriesRepository seriesRepository,
            IEpisodeRepository episodeRepository, TokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tvMaze = tvMaze;
            _seriesRepository = seriesRepository;
            _episodeRepository = episodeRepository;
            _tokenHelper = tokenHelper;

            Name = "UserMutation";

            Field<StringGraphType>(
                "login",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "email" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "password" }
                ),
                resolve: context =>
                {
                    string email = context.GetArgument<string>("email");
                    string pass = context.GetArgument<string>("password");

                    var user = _userRepository.FindByEmail(email);

                    if (user == null)
                        throw new ExecutionError("User not found");

                    if (string.IsNullOrEmpty(user.Password) || !BCr.Verify(pass, user.Password))
                        throw new ExecutionError("Incorrect password");

                    return _tokenHelper.GenerateToken(1);
                });

            Field<UserType>(
                "register",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserInputType>> { Name = "user" }
                ),
                resolve: context =>
                {
                    var userInput = context.GetArgument<User>("user");
                    Console.WriteLine("input: " + userInput.Email);

                    if (_userRepository.FindByEmail(userInput.Email) != null)
                        throw new ExecutionError("Already registered");

                    Console.WriteLine("before construct");
                    var user = ConstructUserFromRegister(userInput);
                    Console.WriteLine("after construct");

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
                    var episode = await _episodeRepository.GetByIdAsync(episodeId);

                    // Get its Series from db or API
                    episode.Series = await _seriesRepository.GetByIdAsync(episode.SeriesId);

                    // Add the episode to the users watched episodes and persist
                    user.AddWatchedEpisode(episode);
                    _userRepository.Update(user);
                    _userRepository.SaveChanges();
                    return user;
                }
            ).AuthorizeWith("UserPolicy"); ;

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
            ).AuthorizeWith("UserPolicy"); ;


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
                    var series = await _seriesRepository.GetByIdAsync(seriesId);

                    // Add the favorite and persist
                    user.AddFavoriteSeries(series);
                    _userRepository.Update(user);
                    _userRepository.SaveChanges();
                    return user;
                }
            ).AuthorizeWith("UserPolicy"); ;

            Field<UserType>(
                "removeFavoriteSeries",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "userId" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "seriesId" }
                ),
                resolve: context =>
                {
                    var user = _userRepository.GetById(context.GetArgument<int>("userId"));
                    var series = user.FavoriteSeries.Single(fs => fs.SeriesId == context.GetArgument<int>("seriesId"));

                    user.FavoriteSeries.Remove(series);

                    _userRepository.Update(user);
                    _userRepository.SaveChanges();

                    return user;
                }
            );

            FieldAsync<UserType>(
                "addSeriesToWatchList",
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

                    // Check if seriesid not already in watchlist
                    if (user.WatchListedSeries.Any(fs => fs.SeriesId == seriesId)) return user;

                    // Get series from db or API
                    var series = await _seriesRepository.GetByIdAsync(seriesId);

                    // Add the favorite and persist
                    user.AddSeriesToWatchList(series);
                    _userRepository.Update(user);
                    _userRepository.SaveChanges();
                    return user;
                }
            ).AuthorizeWith("UserPolicy"); ;

            Field<UserType>(
                "removeSeriesFromWatchList",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "userId" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "seriesId" }
                ),
                resolve: context =>
                {
                    var user = _userRepository.GetById(context.GetArgument<int>("userId"));
                    var series = user.WatchListedSeries.Single(fs => fs.SeriesId == context.GetArgument<int>("seriesId"));

                    user.WatchListedSeries.Remove(series);

                    _userRepository.Update(user);
                    _userRepository.SaveChanges();

                    return user;
                }
            ).AuthorizeWith("UserPolicy"); ;

        }

        private User ConstructUserFromRegister(User userinput) => new User()
        {
            Email = userinput.Email,
            FullName = userinput.FullName,
            Role = "User",
            Password = BCr.HashPassword(userinput.Password, 7)
        };
    }
}
