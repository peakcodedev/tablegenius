using TableGenius.Api.Entities;
using TableGenius.Api.Entities.Company;
using TableGenius.Api.Entities.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TableGenius.Api.Repo.Database.Configurations;

public class ProjectRatingConfiguration : BaseConfiguration<ProjectRating>, IEntityTypeConfiguration<ProjectRating>
{
    public new void Configure(EntityTypeBuilder<ProjectRating> builder)
    {
        base.Configure(builder);
        builder.HasOne(e => e.Project).WithOne(c => c.ProjectRating).HasForeignKey<ProjectRating>(p => p.ProjectId).IsRequired(false);
        builder.HasOne(e => e.ProjectExpert).WithMany(c => c.ProjectRatings).HasForeignKey(p => p.ProjectExpertId).IsRequired(false);
    }
}