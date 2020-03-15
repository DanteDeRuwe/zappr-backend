using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zappr.Api.Domain;

namespace Zappr.Api.Data.Configurations
{
    class UserEpisodeConfiguration : IEntityTypeConfiguration<UserEpisode>
    {
        public void Configure(EntityTypeBuilder<UserEpisode> builder)
        {
            builder.ToTable("UserEpsisode");
            builder.HasKey(ue => new { ue.User, ue.EpisodeId });

            builder.HasOne(ue => ue.User).WithMany(u => u.RatedEpisodes).HasForeignKey(ue => ue.UserId);
            builder.HasOne(ue => ue.User).WithMany(u => u.WatchedEpisodes).HasForeignKey(ue => ue.UserId); ;
            builder.HasOne(ue => ue.Episode).WithMany().HasForeignKey(ue => ue.EpisodeId); ;
        }
    }
}