using TableGenius.Api.Entities.Admin;
using TableGenius.Api.Entities.Default;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TableGenius.Api.Repo.Database.Configurations;

public class UserConfiguration : BaseConfiguration<User>, IEntityTypeConfiguration<User>
{
    public new void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
    }
}