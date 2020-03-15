using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zappr.Api.Domain;

namespace Zappr.Api.Data.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);

            builder.HasMany(u => u.FavoriteSeries).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.WatchList).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.RatedSeries).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.RatedEpisodes).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.WatchedEpisodes).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
