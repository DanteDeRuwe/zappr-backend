using GraphQL.Authorization;
using System.Security.Claims;

namespace Zappr.Api.GraphQL.Helpers
{
    public class GraphQLUserContext : IProvideClaimsPrincipal
    {
        public ClaimsPrincipal User { get; set; }
    }
}


