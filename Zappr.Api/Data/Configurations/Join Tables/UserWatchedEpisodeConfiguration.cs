using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zappr.Api.Domain;

namespace Zappr.Api.Data.Configurations
{
    public class UserWatchedEpisodeConfiguration : IEntityTypeConfiguration<UserWatchedEpisode>
    {
        public void Configure(EntityTypeBuilder<UserWatchedEpisode> builder)
        {
            builder.ToTable("UserWatchedEpisode");
            builder.HasKey(ue => new { ue.UserId, ue.EpisodeId });

            builder.HasOne(ue => ue.User).WithMany(u => u.WatchedEpisodes).HasForeignKey(ue => ue.UserId);
            builder.HasOne(ue => ue.Episode).WithMany().HasForeignKey(ue => ue.EpisodeId); ;
        }
    }
}