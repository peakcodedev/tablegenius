using TableGenius.Api.Entities.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TableGenius.Api.Repo.Database.Configurations;

public class CourseConfiguration : BaseConfiguration<Course>,
    IEntityTypeConfiguration<Course>
{
    public new void Configure(EntityTypeBuilder<Course> builder)
    {
        base.Configure(builder);
        builder.HasOne(e => e.User).WithOne().HasForeignKey<Course>(p => p.UserId)
            .IsRequired(false);
    }
}