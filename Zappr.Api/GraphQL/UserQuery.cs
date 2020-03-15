using GraphQL.Types;
using System.Threading.Tasks;
using Zappr.Api.Data.Repositories;
using Zappr.Api.Domain;
using Zappr.Api.GraphQL.Types;
using Zappr.Api.Services;

namespace Zappr.Api.GraphQL
{
    public class UserQuery : ObjectGraphType
    {
        private readonly IUserRepository _userRepository;
        public UserQuery(IUserRepository userRepository)
        {
            _userRepository = userRepository;


            // get all
            Field<ListGraphType<UserType>>("users", resolve: context => _userRepository.GetAll());

            // get by id
            QueryArguments args = new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" });
            Field<UserType>(
                "user",
                arguments: args,
                resolve: context => _userRepository.GetUserById(context.GetArgument<int>("id"))
            );

            Field<UserType>(
                "randomuser",
                arguments: args,
                resolve: context =>
                {
                    User u = _userRepository.GetUserById(context.GetArgument<int>("id"));
                    return EnrichUserAsync(u);
                }
            );

        }

        private async Task<User> EnrichUserAsync(User u)
        {
            u.FullName = await TvdbService.GetRandomName();
            return u;
        }
    }
}
