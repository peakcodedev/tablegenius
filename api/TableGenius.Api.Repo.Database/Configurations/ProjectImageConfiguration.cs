using TableGenius.Api.Entities.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TableGenius.Api.Repo.Database.Configurations;

public class ProjectImageConfiguration : BaseConfiguration<ProjectImage>, IEntityTypeConfiguration<ProjectImage>
{
    public void Configure(EntityTypeBuilder<ProjectImage> builder)
    {
        base.Configure(builder);
        builder.HasOne(e => e.Project).WithMany(e => e.ProjectImages).HasForeignKey(p => p.ProjectId)
            .IsRequired(false);
    }
}