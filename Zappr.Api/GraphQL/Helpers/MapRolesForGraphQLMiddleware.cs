using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Zappr.Api.GraphQL.Helpers
{
    public class MapRolesForGraphQLMiddleware
    {
        private readonly RequestDelegate _next;

        public MapRolesForGraphQLMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            var metadata = context.User.Claims.SingleOrDefault(x => x.Type.Equals("metadata"));

            if (metadata != null)
            {
                var roleContainer = JsonConvert.DeserializeObject<RoleContainer>(metadata.Value);
                (context.User.Identity as ClaimsIdentity).AddClaim(new Claim("Role",
                    string.Join(", ", roleContainer.Roles)));
            }

            await _next(context);
        }
    }
}