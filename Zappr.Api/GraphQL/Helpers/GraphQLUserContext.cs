using GraphQL.Authorization;
using System.Linq;
using System.Security.Claims;

namespace Zappr.Api.GraphQL.Helpers
{
    public class GraphQLUserContext : IProvideClaimsPrincipal
    {
        public ClaimsPrincipal User { get; set; }

        public int UserId => int.Parse(User.Claims.First(c => c.Type.Equals("Id"))?.Value);
    }
}
