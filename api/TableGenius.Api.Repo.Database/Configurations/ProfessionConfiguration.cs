using TableGenius.Api.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TableGenius.Api.Repo.Database.Configurations;

public class ProfessionConfiguration : BaseConfiguration<Profession>, IEntityTypeConfiguration<Profession>
{
    public new void Configure(EntityTypeBuilder<Profession> builder)
    {
        base.Configure(builder);
    }
}