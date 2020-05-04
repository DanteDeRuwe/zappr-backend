using GraphQL.Types;
using Zappr.Api.Data.Repositories;
using Zappr.Api.Domain;
using Zappr.Api.GraphQL.Types;

namespace Zappr.Api.GraphQL
{
    public class UserQuery : ObjectGraphType
    {
        private readonly UserRepository _userRepository;
        public UserQuery(IUserRepository userRepository)
        {
            Name = "UserQuery";
            _userRepository = (UserRepository)userRepository;


            // get all
            Field<ListGraphType<UserType>>("all", resolve: context => _userRepository.GetAll());

            // get by id
            QueryArguments args = new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" });
            Field<UserType>(
                "get",
                arguments: args,
                resolve: context => _userRepository.GetById(context.GetArgument<int>("id"))
            );

        }

    }
}
