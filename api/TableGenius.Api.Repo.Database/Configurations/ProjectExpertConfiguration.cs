using TableGenius.Api.Entities;
using TableGenius.Api.Entities.Company;
using TableGenius.Api.Entities.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TableGenius.Api.Repo.Database.Configurations;

public class ProjectExpertConfiguration : BaseConfiguration<ProjectExpert>, IEntityTypeConfiguration<ProjectExpert>
{
    public new void Configure(EntityTypeBuilder<ProjectExpert> builder)
    {
        base.Configure(builder);
        builder.HasMany(e => e.ProjectRatings).WithOne(c => c.ProjectExpert).HasForeignKey(p => p.ProjectExpertId).IsRequired(false);
    }
}