using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zappr.Core.Domain;

namespace Zappr.Infrastructure.Data.Configurations
{
    public class UserWatchlistedSeriesConfiguration : IEntityTypeConfiguration<UserWatchListedSeries>
    {
        public void Configure(EntityTypeBuilder<UserWatchListedSeries> builder)
        {
            builder.ToTable("UserWatchListedSeries");
            builder.HasKey(us => new { us.UserId, us.SeriesId });

            builder.HasOne(us => us.User).WithMany(u => u.WatchListedSeries).HasForeignKey(us => us.UserId);
            builder.HasOne(us => us.Series).WithMany().HasForeignKey(us => us.SeriesId);
        }
    }
}