using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Repo.Database.Configurations;

public abstract class TenantBaseConfiguration<T>(RepositoryContext repositoryContext)
    : BaseConfiguration<T>, IEntityTypeConfiguration<T>
    where T : TenantBase
{
    public new void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);
        builder.HasQueryFilter(mt => mt.TenantId == repositoryContext.TenantProvider.GetTenantId());
    }
}