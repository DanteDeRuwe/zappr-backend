using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zappr.Api.Domain;

namespace Zappr.Api.Data.Configurations
{
    public class UserFavoriteSeriesConfiguration : IEntityTypeConfiguration<UserSeries>
    {
        public void Configure(EntityTypeBuilder<UserSeries> builder)
        {
            builder.ToTable("UserFavoriteSeries");
            builder.HasKey(us => new { us.UserId, us.SeriesId });

            builder.HasOne(us => us.User).WithMany(u => u.FavoriteSeries).HasForeignKey(us => us.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(us => us.Series).WithMany().HasForeignKey(us => us.SeriesId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}