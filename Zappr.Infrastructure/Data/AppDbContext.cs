using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Zappr.Core.Entities;

namespace Zappr.Infrastructure.Data

{
    public class AppDbContext : DbContext
    {
        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Episode> Episodes { get; set; }

        //public DbSet<Comment> Comments { get; set; }
        //public DbSet<Rating> Ratings { get; set; }


        // Constructor 
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options) { }

        // finds all classes that implement IEntityTypeConfiguration and applies their configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);



    }
}
