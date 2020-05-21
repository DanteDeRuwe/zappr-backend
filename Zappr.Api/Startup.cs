using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Zappr.Api.Data;
using Zappr.Api.Data.Repositories;
using Zappr.Api.Domain;
using Zappr.Api.GraphQL;
using Zappr.Api.GraphQL.Helpers;
using Zappr.Api.GraphQL.Mutations;
using Zappr.Api.GraphQL.Types;
using Zappr.Api.Helpers;
using Zappr.Api.Services;

namespace Zappr.Api
{
    public class Startup
    {

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //CORS
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                        .WithMethods("GET", "POST")
                        .WithOrigins("http://localhost:4200");
                });
            });

            //DI
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            //Making things work ¯\_( ")_/¯
            services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);
            services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);

            // DB
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppDbContext"))
            );


            // HTTPContext
            services.AddHttpContextAccessor();

            //External APIs
            services.AddSingleton<TVMazeService>();

            //Repos
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISeriesRepository, SeriesRepository>();
            services.AddTransient<IEpisodeRepository, EpisodeRepository>();

            //Schema
            services.AddTransient<ISchema, ZapprSchema>();

            // Types
            services.AddScoped<SeriesType>();
            services.AddScoped<UserType>();
            services.AddScoped<EpisodeType>();
            services.AddScoped<CommentType>();
            services.AddScoped<RatingType>();

            // Input Types
            services.AddScoped<UserInputType>();

            // Queries
            services.AddScoped<UserQuery>();
            services.AddScoped<SeriesQuery>();
            services.AddScoped<ZapprQuery>();

            // Mutations
            services.AddScoped<UserMutation>();
            services.AddScoped<SeriesMutation>();
            services.AddScoped<ZapprMutation>();


            //JWT
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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("JWT")["secret"]))
                    };
                });

            services.AddAuthorization();


            // GraphQL
            services.AddGraphQL().AddUserContextBuilder(context => new GraphQLUserContext { User = context.User });

            //GraphQLAuthorization
            services.AddGraphQLAuth(_ =>
            {
                _.AddPolicy("UserPolicy", p => p.RequireClaim("Role", new string[] { "User", "Admin" }));
                _.AddPolicy("AdminPolicy", p => p.RequireClaim("Role", "Admin"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            //context.Database.Migrate();

            app.UseCors("DefaultPolicy");

            app.UseAuthentication();

            app.UseGraphQL<ISchema>("/graphql");

            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/"
            });
        }
    }
}
