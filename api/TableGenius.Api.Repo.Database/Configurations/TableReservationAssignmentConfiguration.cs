using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableGenius.Api.Entities.Reservations;

namespace TableGenius.Api.Repo.Database.Configurations;

public class TableReservationAssignmentConfiguration(RepositoryContext repositoryContext) :
    IEntityTypeConfiguration<TableReservationAssignment>
{
    public void Configure(EntityTypeBuilder<TableReservationAssignment> builder)
    {
        builder.HasKey(bc => new {bc.TableId, bc.ReservationAssignmentId, bc.TenantId});
        builder.HasOne(bc => bc.Table)
            .WithMany(b => b.TableReservationAssignments)
            .HasForeignKey(bc => bc.TableId);
        builder.HasOne(bc => bc.ReservationAssignment)
            .WithMany(c => c.TableReservationAssignments)
            .HasForeignKey(bc => bc.ReservationAssignmentId);
        builder.ToTable("TableReservationAssignments");
        builder.HasQueryFilter(mt => mt.TenantId == repositoryContext.TenantProvider.GetTenantId());
    }
}