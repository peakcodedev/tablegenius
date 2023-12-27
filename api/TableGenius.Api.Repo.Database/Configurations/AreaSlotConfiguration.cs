using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableGenius.Api.Entities.Place;

namespace TableGenius.Api.Repo.Database.Configurations;

public class AreaSlotConfiguration(RepositoryContext repositoryContext)
    : TenantBaseConfiguration<AreaSlot>(repositoryContext),
        IEntityTypeConfiguration<AreaSlot>
{
    public new void Configure(EntityTypeBuilder<AreaSlot> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.Length).IsRequired(false);
        builder.Property(b => b.Type).IsRequired();
        builder.Property(b => b.Name).IsRequired();
        builder.Property(b => b.Start).IsRequired(false);
        builder.Property(b => b.End).IsRequired(false);
        builder.HasOne(e => e.Area).WithMany(c => c.AreaSlots).HasForeignKey(p => p.AreaId)
            .IsRequired();
        builder.ToTable("AreaSlots");
    }
}