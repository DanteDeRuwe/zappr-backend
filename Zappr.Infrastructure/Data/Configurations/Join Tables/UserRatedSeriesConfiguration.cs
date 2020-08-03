using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zappr.Core.Entities;

namespace Zappr.Infrastructure.Data.Configurations
{
    public class UserRatedSeriesConfiguration : IEntityTypeConfiguration<UserRatedSeries>
    {
        public void Configure(EntityTypeBuilder<UserRatedSeries> builder)
        {
            builder.ToTable("UserRatedSeries");
            builder.HasKey(us => new { us.UserId, us.SeriesId });

            builder.HasOne(us => us.User).WithMany(u => u.RatedSeries).HasForeignKey(us => us.UserId);
            builder.HasOne(us => us.Series).WithMany().HasForeignKey(us => us.SeriesId);
        }
    }
}