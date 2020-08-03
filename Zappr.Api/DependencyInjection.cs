using GraphQL.Authorization;
using GraphQL.Server;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Zappr.Api.GraphQL;
using Zappr.Api.GraphQL.Mutations;
using Zappr.Api.GraphQL.Queries;
using Zappr.Api.GraphQL.Types;
using Zappr.Api.GraphQL.Types.InputTypes;
using Zappr.Api.Helpers;

namespace Zappr.Api
{
    public static class DependencyInjection
    {

        public static void AddGraphQLWithDependencies(this IServiceCollection services)
        {
            services.AddTransient<ISchema, ZapprSchema>();

            services.AddScoped<SeriesType>();
            services.AddScoped<UserType>();
            services.AddScoped<EpisodeType>();
            services.AddScoped<CommentType>();
            services.AddScoped<RatingType>();

            services.AddScoped<UserInputType>();

            services.AddScoped<UserQuery>();
            services.AddScoped<SeriesQuery>();
            services.AddScoped<ZapprQuery>();

            services.AddScoped<UserMutation>();
            services.AddScoped<SeriesMutation>();
            services.AddScoped<ZapprMutation>();

            services.AddGraphQL().AddUserContextBuilder(context => new GraphQLUserContext { User = context.User });
        }


        public static void AddTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<TokenHelper>();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JWT")["secret"]))
                    };
                });
        }

        public static void AddGraphQLAuth(this IServiceCollection services, Action<AuthorizationSettings> configure)
        {
            services.AddHttpContextAccessor();
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

        public static void AddCorsWithDefaultPolicy(this IServiceCollection services, string origin) => services.AddCors(options =>
           {
               options.AddPolicy("DefaultPolicy", builder =>
               {
                   builder.AllowAnyHeader()
                       .WithMethods("GET", "POST")
                       .WithOrigins(origin);
               });
           });

        public static void AllowSynchronousIO(this IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);
            services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
        }
    }
}
