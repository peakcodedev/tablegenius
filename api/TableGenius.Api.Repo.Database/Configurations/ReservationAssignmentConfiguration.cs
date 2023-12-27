using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableGenius.Api.Entities.Reservations;

namespace TableGenius.Api.Repo.Database.Configurations;

public class ReservationAssignmentConfiguration(RepositoryContext repositoryContext)
    : TenantBaseConfiguration<ReservationAssignment>(repositoryContext),
        IEntityTypeConfiguration<ReservationAssignment>
{
    public new void Configure(EntityTypeBuilder<ReservationAssignment> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.BookingDate).IsRequired();
        builder.HasOne(e => e.Reservation).WithOne(c => c.ReservationAssignment)
            .HasForeignKey<ReservationAssignment>(b => b.ReservationId).IsRequired(false);
        builder.ToTable("ReservationAssignments");
    }
}