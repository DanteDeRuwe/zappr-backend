using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Zappr.Api.Data.Configurations;
using Zappr.Api.Domain;

namespace Zappr.Api.Data

{
    public class AppDbContext : DbContext
    {
        // DbSets
        public DbSet<User> Users { get; set; }
        //public DbSet<Comment> Comments { get; set; }
        //public DbSet<Rating> Ratings { get; set; }


        // Constructor 
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new EpisodeConfiguration());
            builder.ApplyConfiguration(new SeriesConfiguration());

            builder.ApplyConfiguration(new UserFavoriteSeriesConfiguration());
            //builder.ApplyConfiguration(new UserWatchlistConfiguration()); TODO: make other joined entity
            builder.ApplyConfiguration(new UserWatchedEpisodeConfiguration());

            //builder.ApplyConfiguration(new CommentConfiguration());
            //builder.ApplyConfiguration(new RatingConfiguration());

        }
    }
}
