using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zappr.Api.Domain;

namespace Zappr.Api.Data.Configurations
{
    public class UserWatchlistConfiguration : IEntityTypeConfiguration<UserSeries>
    {
        public void Configure(EntityTypeBuilder<UserSeries> builder)
        {
            builder.ToTable("UserWatchList");
            builder.HasKey(us => new { us.UserId, us.SeriesId });

            builder.HasOne(us => us.User).WithMany(u => u.WatchList).HasForeignKey(us => us.UserId);
            builder.HasOne(us => us.Series).WithMany().HasForeignKey(us => us.SeriesId);
        }
    }
}