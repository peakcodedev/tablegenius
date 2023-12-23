using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableGenius.Api.Entities.Place;

namespace TableGenius.Api.Repo.Database.Configurations;

public class LocationConfiguration : BaseConfiguration<Location>,
    IEntityTypeConfiguration<Location>
{
    public new void Configure(EntityTypeBuilder<Location> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.Description).HasMaxLength(1024).IsRequired(false);
        builder.Property(b => b.Name).HasMaxLength(128).IsRequired();
        builder.Property(b => b.Address).HasMaxLength(512).IsRequired(false);
        builder.Property(b => b.LogoUrl).HasMaxLength(1024).IsRequired(false);
    }
}