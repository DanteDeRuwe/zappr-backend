using GraphQL.Authorization;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Transports.AspNetCore.Internal;
using GraphQL.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Zappr.Api.GraphQL.Helpers
{
    public static class GraphQLAuthExtensions
    {
        public static void AddGraphQLAuth(this IServiceCollection services, Action<AuthorizationSettings> configure)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IAuthorizationEvaluator, AuthorizationEvaluator>();
            services.AddTransient<IValidationRule, AuthorizationValidationRule>();

            services.AddTransient<IUserContextBuilder>(s => new UserContextBuilder<GraphQLUserContext>(context =>
            {
                GraphQLUserContext userContext = new GraphQLUserContext { User = context.User };

                return Task.FromResult(userContext);
            }));

            services.AddSingleton(s =>
            {
                AuthorizationSettings authSettings = new AuthorizationSettings();
                configure(authSettings);
                return authSettings;
            });
        }
    }
}