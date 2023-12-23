using TableGenius.Api.Entities.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TableGenius.Api.Repo.Database.Configurations;

public class ProjectUserRatingConfiguration : BaseConfiguration<ProjectUserRating>,
    IEntityTypeConfiguration<ProjectUserRating>
{
    public new void Configure(EntityTypeBuilder<ProjectUserRating> builder)
    {
        base.Configure(builder);
        builder.HasOne(e => e.User).WithOne(c => c.ProjectUserRating).HasForeignKey<ProjectUserRating>(p => p.UserId)
            .IsRequired();
    }
}