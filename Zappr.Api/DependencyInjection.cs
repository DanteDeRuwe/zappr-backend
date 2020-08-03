using GraphQL.Authorization;
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

namespace Zappr.Api
{
    public static class DependencyInjection
    {
        public static void AddTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
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
