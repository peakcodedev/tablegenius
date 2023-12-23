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
    }
}