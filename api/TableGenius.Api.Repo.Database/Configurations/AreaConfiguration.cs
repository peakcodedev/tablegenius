using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableGenius.Api.Entities.Place;
using TableGenius.Api.Repo.Database.Providers;

namespace TableGenius.Api.Repo.Database.Configurations;

public class AreaConfiguration(TenantProvider tenantProvider) : TenantBaseConfiguration<Area>(tenantProvider),
    IEntityTypeConfiguration<Area>
{
    public new void Configure(EntityTypeBuilder<Area> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.Name).HasMaxLength(128).IsRequired();
        builder.Property(b => b.BlueprintUrl).HasMaxLength(1024).IsRequired(false);
        builder.ToTable("Areas");
    }
}