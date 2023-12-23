using TableGenius.Api.Entities.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TableGenius.Api.Repo.Database.Configurations;

public class ProjectCollaborationConfiguration : BaseConfiguration<ProjectCollaboration>,
    IEntityTypeConfiguration<ProjectCollaboration>
{
    public new void Configure(EntityTypeBuilder<ProjectCollaboration> builder)
    {
        base.Configure(builder);
        builder.HasOne(e => e.Project).WithMany(e => e.ProjectCollaborations).HasForeignKey(p => p.ProjectId)
            .IsRequired(false);
        builder.HasOne(e => e.User).WithOne().HasForeignKey<ProjectCollaboration>(p => p.UserId)
            .IsRequired(false);
    }
}