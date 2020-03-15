using GraphQL.Types;
using Zappr.Api.Domain;

namespace Zappr.Api.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(u => u.Id);
            Field(u => u.FullName);

        }
    }
}
