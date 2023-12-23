using TableGenius.Api.Entities;
using TableGenius.Api.Entities.Company;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TableGenius.Api.Repo.Database.Configurations;

public class CompanyConfiguration : BaseConfiguration<Company>, IEntityTypeConfiguration<Company>
{
    public new void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);
    }
}