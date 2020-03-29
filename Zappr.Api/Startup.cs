using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zappr.Api.Data;
using Zappr.Api.Data.Repositories;
using Zappr.Api.Domain;
using Zappr.Api.GraphQL;
using Zappr.Api.GraphQL.Mutations;
using Zappr.Api.GraphQL.Types;
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

            //Schema
            services.AddTransient<ISchema, ZapprSchema>();

            // Types
            services.AddScoped<SeriesType>();
            services.AddScoped<UserType>();
            services.AddScoped<UserInputType>();

            // Queries
            services.AddScoped<UserQuery>();
            services.AddScoped<SeriesQuery>();
            services.AddScoped<ZapprQuery>();

            // Mutations
            services.AddScoped<UserMutation>();
            services.AddScoped<ZapprMutation>();

            // GraphQL
            services.AddGraphQL();

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
            context.Database.Migrate();

            app.UseCors("DefaultPolicy");

            app.UseGraphQL<ISchema>("/graphql");

            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/"
            });
        }
    }
}
