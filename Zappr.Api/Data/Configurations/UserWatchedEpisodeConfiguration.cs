using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zappr.Api.Domain;

namespace Zappr.Api.Data.Configurations
{
    public class UserWatchedEpisodeConfiguration : IEntityTypeConfiguration<UserEpisode>
    {
        public void Configure(EntityTypeBuilder<UserEpisode> builder)
        {
            builder.ToTable("UserWatchedEpsisode");
            builder.HasKey(ue => new { ue.UserId, ue.EpisodeId });

            builder.HasOne(ue => ue.User).WithMany(u => u.WatchedEpisodes).HasForeignKey(ue => ue.UserId);
            builder.HasOne(ue => ue.Episode).WithMany().HasForeignKey(ue => ue.EpisodeId); ;
        }
    }
}