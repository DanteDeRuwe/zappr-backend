using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zappr.Api.Domain;

namespace Zappr.Api.Data.Configurations
{
    class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Rating");
            builder.HasKey(r => r.Id);

            builder.HasOne(c => c.Author).WithMany();
        }
    }
}