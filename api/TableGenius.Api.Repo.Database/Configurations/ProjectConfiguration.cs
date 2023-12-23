using TableGenius.Api.Entities;
using TableGenius.Api.Entities.Company;
using TableGenius.Api.Entities.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TableGenius.Api.Repo.Database.Configurations;

public class ProjectConfiguration : BaseConfiguration<Project>, IEntityTypeConfiguration<Project>
{
    public new void Configure(EntityTypeBuilder<Project> builder)
    {
        base.Configure(builder);
        builder.HasOne(e => e.ProjectRating).WithOne(c => c.Project).HasForeignKey<ProjectRating>(p => p.ProjectId).IsRequired(false);
        builder.HasOne(e => e.ProjectBooth).WithOne(c => c.Project).HasForeignKey<ProjectBooth>(p => p.ProjectId).IsRequired(false);
    }
}