using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TableGenius.Api.Repo.Database.Configurations;
using TableGenius.Api.Repo.Database.Providers;
using TableGenius.Api.Settings;

namespace TableGenius.Api.Repo.Database;

public class RepositoryContext : DbContext
{
    private readonly IOptions<DatabaseOptions> _databaseOptions;
    public TenantProvider TenantProvider;

    public RepositoryContext(IOptions<DatabaseOptions> options, TenantProvider tenantProvider)
    {
        ContextId = Guid.NewGuid();
        _databaseOptions = options;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_databaseOptions.Value.Database,
            x => x.MigrationsAssembly("TableGenius.Api.Repo.Database"));
    }
}