using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableGenius.Api.Entities.Place;

namespace TableGenius.Api.Repo.Database.Configurations;

public class AreaConfiguration : BaseConfiguration<Area>,
    IEntityTypeConfiguration<Area>
{
    public new void Configure(EntityTypeBuilder<Area> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.Name).HasMaxLength(128).IsRequired();
        builder.Property(b => b.BlueprintUrl).HasMaxLength(1024).IsRequired(false);
    }
}