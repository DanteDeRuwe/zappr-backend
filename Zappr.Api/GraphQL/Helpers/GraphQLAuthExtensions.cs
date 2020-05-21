using GraphQL.Authorization;
using GraphQL.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

public static class GraphQLAuthExtensions
{

    public static void AddGraphQLAuth(this IServiceCollection services, Action<AuthorizationSettings> configure)
    {
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.TryAddSingleton<IAuthorizationEvaluator, AuthorizationEvaluator>();
        services.AddTransient<IValidationRule, AuthorizationValidationRule>();

        services.TryAddTransient(s =>
        {
            AuthorizationSettings authSettings = new AuthorizationSettings();
            configure(authSettings);
            return authSettings;
        });
    }
}