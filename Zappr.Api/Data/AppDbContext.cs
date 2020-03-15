using Microsoft.EntityFrameworkCore;
using Zappr.Api.Data.Configurations;
using Zappr.Api.Domain;

namespace Zappr.Api.Data

{
    public class AppDbContext : DbContext
    {
        // DbSets
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserEpisode> UserEpisodes { get; set; }
        public DbSet<UserSeries> UserSeries { get; set; }


        // Constructor 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new EpisodeConfiguration());
            builder.ApplyConfiguration(new SeriesConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserEpisodeConfiguration());
            builder.ApplyConfiguration(new UserSeriesConfiguration());
        }
    }
}
