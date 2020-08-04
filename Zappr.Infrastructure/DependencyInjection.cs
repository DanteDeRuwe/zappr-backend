using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Zappr.Application.GraphQL.Interfaces;
using Zappr.Core.Interfaces;
using Zappr.Infrastructure.Data;
using Zappr.Infrastructure.Data.Repositories;
using Zappr.Infrastructure.Services;

namespace Zappr.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AppDbContext"))
            );

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISeriesRepository, SeriesRepository>();
            services.AddTransient<IEpisodeRepository, EpisodeRepository>();

            services.AddHttpClient<ISeriesService, TvMazeSeriesService>(client => client.BaseAddress = new Uri("https://api.tvmaze.com/"));
            services.AddHttpClient<IEpisodeService, TvMazeEpisodeService>(client => client.BaseAddress = new Uri("https://api.tvmaze.com/"));
        }
    }
}
