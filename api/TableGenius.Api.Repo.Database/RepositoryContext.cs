using System;
using Microsoft.EntityFrameworkCore;
using TableGenius.Api.Repo.Database.Configurations;
using TableGenius.Api.Repo.Database.Providers;

namespace TableGenius.Api.Repo.Database;

public class RepositoryContext : DbContext
{
    public TenantProvider TenantProvider;

    public RepositoryContext(DbContextOptions<RepositoryContext> options, TenantProvider tenantProvider) : base(options)
    {
        ContextId = Guid.NewGuid();
        TenantProvider = tenantProvider;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public Guid ContextId { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LocationConfiguration());
        modelBuilder.ApplyConfiguration(new LocationAssignmentConfiguration());
        modelBuilder.ApplyConfiguration(new AreaConfiguration(this));
        modelBuilder.ApplyConfiguration(new TableConfiguration(this));
        modelBuilder.ApplyConfiguration(new ReservationConfiguration(this));
        modelBuilder.ApplyConfiguration(new AreaSlotConfiguration(this));
        modelBuilder.ApplyConfiguration(new ReservationAssignmentConfiguration(this));
        modelBuilder.ApplyConfiguration(new TableReservationAssignmentConfiguration(this));
        base.OnModelCreating(modelBuilder);
    }
}