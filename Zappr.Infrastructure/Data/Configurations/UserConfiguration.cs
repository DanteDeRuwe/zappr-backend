using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zappr.Core.Entities;

namespace Zappr.Infrastructure.Data.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);

            //Series
            builder.HasMany(u => u.FavoriteSeries).WithOne(ufs => ufs.User).HasForeignKey(u => u.UserId); ;
            builder.HasMany(u => u.WatchListedSeries).WithOne(uws => uws.User).HasForeignKey(u => u.UserId); ;
            builder.HasMany(u => u.RatedSeries).WithOne(urs => urs.User).HasForeignKey(u => u.UserId); ;

            //Episodes
            builder.HasMany(u => u.RatedEpisodes).WithOne(ure => ure.User).HasForeignKey(u => u.UserId);
            builder.HasMany(u => u.WatchedEpisodes).WithOne(uwe => uwe.User).HasForeignKey(u => u.UserId); ;
        }
    }
}
