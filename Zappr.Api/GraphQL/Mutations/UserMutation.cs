using GraphQL.Types;
using Zappr.Api.Domain;
using Zappr.Api.GraphQL.Types;

namespace Zappr.Api.GraphQL.Mutations
{
    public class UserMutation : ObjectGraphType
    {
        private readonly IUserRepository _userRepository;


        public UserMutation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
        }



    }
}
