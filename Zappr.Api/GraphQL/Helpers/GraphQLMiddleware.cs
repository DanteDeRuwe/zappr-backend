using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Zappr.Api.Helpers;

namespace Zappr.Api.GraphQL.Helpers
{
    public class GraphQLMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenHelper _tokenHelper;


        public GraphQLMiddleware(RequestDelegate next, TokenHelper tokenHelper)
        {
            _next = next;
            _tokenHelper = tokenHelper;

        }

        public async Task Invoke(HttpContext context)
        {

            Console.WriteLine(context.User.Identity.IsAuthenticated);

            //var result = await context.AuthenticateAsync("Bearer");
            string bearer = context.Request.Headers["Authorization"].ToString();

            if (context.User.Identity.IsAuthenticated || string.IsNullOrEmpty(bearer) || !bearer.StartsWith("Bearer "))
            {
                await _next(context);
                return;
            }

            string token = bearer.Split(" ")[1];

            //testing
            bool result = _tokenHelper.ValidateCurrentToken(token);

            Console.WriteLine("RESULT | " + result);
            if (result)
            {
                Console.WriteLine(_tokenHelper.GetClaim(token, "Id"));
                Console.WriteLine(_tokenHelper.GetClaim(token, "Role"));
            }


            if (!IsGraphQLRequest(context))
            {
                await _next(context);
                return;
            }

            if (result)
            {
                await ExecuteAsync(context,
                    _tokenHelper.GetClaim(token, "Id"),
                    _tokenHelper.GetClaim(token, "Role")
                    );
            }
            else
            {
                await _next(context);
            }
        }

        private bool IsGraphQLRequest(HttpContext context) => context.Request.Path.StartsWithSegments("/graphql")
                && string.Equals(context.Request.Method, "POST", StringComparison.OrdinalIgnoreCase);


        private async Task ExecuteAsync(HttpContext context, string userId, string role)
        {
            ClaimsIdentity identity = new ClaimsIdentity(role + " Identity");

            identity.AddClaim(new Claim("Id", userId));
            identity.AddClaim(new Claim("role", role));

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            Console.WriteLine("auth? " + principal.Identity.IsAuthenticated);
            Console.WriteLine("user? " + principal.HasClaim("role", "User"));
            Console.WriteLine("claims? " + string.Join(',', principal.Claims.Select(c => c.Type + " " + c.Value)));

            await _next(context);
        }



    }
}