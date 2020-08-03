using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zappr.Core.Entities;

namespace Zappr.Infrastructure.Data.Configurations
{
    public class UserRatedEpisodeConfiguration : IEntityTypeConfiguration<UserRatedEpisode>
    {
        public void Configure(EntityTypeBuilder<UserRatedEpisode> builder)
        {
            builder.ToTable("UserRatedEpisode");
            builder.HasKey(ue => new { ue.UserId, ue.EpisodeId });

            builder.HasOne(ue => ue.User).WithMany(u => u.RatedEpisodes).HasForeignKey(ue => ue.UserId);
            builder.HasOne(ue => ue.Episode).WithMany().HasForeignKey(ue => ue.EpisodeId);
        }
    }
}