using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableGenius.Api.Entities.Default;
using TableGenius.Api.Repo.Database.Providers;

namespace TableGenius.Api.Repo.Database.Configurations;

public abstract class TenantBaseConfiguration<T>(TenantProvider tenantProvider)
    : BaseConfiguration<T>, IEntityTypeConfiguration<T>
    where T : TenantBase
{
    public new void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);
        builder.HasQueryFilter(mt => mt.TenantId == tenantProvider.GetTenantId());
    }
}