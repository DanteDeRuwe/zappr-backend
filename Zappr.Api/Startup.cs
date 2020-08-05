using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zappr.Application;
using Zappr.Infrastructure;

namespace Zappr.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsWithDefaultPolicy(Configuration);
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AllowSynchronousIO();

            services.AddInfrastructure(Configuration);
            services.AddApplication();

            services.AddTokenAuthentication(Configuration);
            services.AddAuthorization();

            services.AddGraphQLAuth(_ =>
            {
                _.AddPolicy("UserPolicy", p => p.RequireClaim("Role", new string[] { "User", "Admin" }));
                _.AddPolicy("AdminPolicy", p => p.RequireClaim("Role", "Admin"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors("DefaultPolicy");

            app.UseAuthentication();

            app.UseGraphQL<ISchema>("/graphql");
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions { Path = "/" });
        }
    }
}
