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

            //Series
            builder.HasMany(u => u.FavoriteSeries).WithOne(); ;
            builder.HasMany(u => u.WatchList).WithOne();
            builder.HasMany(u => u.RatedSeries).WithOne();

            //Episodes
            builder.HasMany(u => u.RatedEpisodes).WithOne();
            builder.HasMany(u => u.WatchedEpisodes).WithOne();
        }
    }
}
