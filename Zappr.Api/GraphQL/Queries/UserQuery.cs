using GraphQL.Authorization;
using GraphQL.Types;
using Zappr.Api.GraphQL.Helpers;
using Zappr.Api.GraphQL.Types;
using Zappr.Core.Interfaces;
using Zappr.Infrastructure.Data.Repositories;

namespace Zappr.Api.GraphQL
{
    public class UserQuery : ObjectGraphType
    {
        private readonly UserRepository _userRepository;
        public UserQuery(IUserRepository userRepository)
        {
            Name = "UserQuery";
            _userRepository = (UserRepository)userRepository;


            // Default: only users can access these queries
            this.AuthorizeWith("UserPolicy");

            // get user information
            Field<UserType>(
            "me",
                resolve: context =>
                {
                    int id = (context.UserContext as GraphQLUserContext).UserId;
                    return _userRepository.GetById(id);
                }
            );

            // get all
            Field<ListGraphType<UserType>>("all", resolve: context => _userRepository.GetAll()).AuthorizeWith("AdminPolicy"); //Admin only

            // get by id
            QueryArguments args = new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" });
            Field<UserType>(
                "get",
                arguments: args,
                resolve: context => _userRepository.GetById(context.GetArgument<int>("id"))
            ).AuthorizeWith("AdminPolicy"); //Admin only
        }

    }
}
