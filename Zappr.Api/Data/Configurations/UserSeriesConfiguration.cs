using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zappr.Api.Domain;

namespace Zappr.Api.Data.Configurations
{
    class UserSeriesConfiguration : IEntityTypeConfiguration<UserSeries>
    {
        public void Configure(EntityTypeBuilder<UserSeries> builder)
        {
            builder.ToTable("UserSeries");
            builder.HasKey(ue => new { ue.User, ue.SeriesId });

            builder.HasOne(ue => ue.User).WithMany(u => u.FavoriteSeries).HasForeignKey(ue => ue.UserId);
            builder.HasOne(ue => ue.User).WithMany(u => u.RatedSeries).HasForeignKey(ue => ue.UserId);
            builder.HasOne(ue => ue.User).WithMany(u => u.WatchList).HasForeignKey(ue => ue.UserId);
            builder.HasOne(ue => ue.Series).WithMany().HasForeignKey(ue => ue.SeriesId);
        }
    }
}