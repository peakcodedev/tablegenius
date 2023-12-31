﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableGenius.Api.Entities.Place;

namespace TableGenius.Api.Repo.Database.Configurations;

public class TableConfiguration(RepositoryContext repositoryContext)
    : TenantBaseConfiguration<Table>(repositoryContext),
        IEntityTypeConfiguration<Table>
{
    public new void Configure(EntityTypeBuilder<Table> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.TableNumber).IsRequired();
        builder.Property(b => b.Capacity).IsRequired();
        builder.Property(b => b.Description).HasMaxLength(1024).IsRequired(false);
        builder.HasOne(e => e.Area).WithMany(c => c.Tables).HasForeignKey(p => p.AreaId)
            .IsRequired();
        builder.ToTable("Tables");
    }
}