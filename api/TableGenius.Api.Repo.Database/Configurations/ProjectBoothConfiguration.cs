using TableGenius.Api.Entities;
using TableGenius.Api.Entities.Company;
using TableGenius.Api.Entities.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TableGenius.Api.Repo.Database.Configurations;

public class ProjectBoothConfiguration : BaseConfiguration<ProjectBooth>, IEntityTypeConfiguration<ProjectBooth>
{
    public new void Configure(EntityTypeBuilder<ProjectBooth> builder)
    {
        base.Configure(builder);
        builder.HasOne(e => e.Project).WithOne(c => c.ProjectBooth).HasForeignKey<ProjectBooth>(p => p.ProjectId).IsRequired(false);
    }
}