using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableGenius.Api.Entities.Place;

namespace TableGenius.Api.Repo.Database.Configurations;

public class LocationAssignmentConfiguration : BaseConfiguration<LocationAssignment>,
    IEntityTypeConfiguration<LocationAssignment>
{
    public new void Configure(EntityTypeBuilder<LocationAssignment> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.Mail).HasMaxLength(1024).IsRequired();
        builder.HasOne(e => e.Location).WithMany(c => c.LocationAssignments).HasForeignKey(p => p.LocationId)
            .IsRequired();
        builder.ToTable("LocationAssignments");
    }
}