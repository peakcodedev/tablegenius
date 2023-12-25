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

    public RepositoryContext(IOptions<DatabaseOptions> options, TenantProvider tenantProvider)
    {
        ContextId = Guid.NewGuid();
        TenantProvider = tenantProvider;
        _databaseOptions = options;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public Guid ContextId { get; }

    public TenantProvider TenantProvider { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LocationConfiguration());
        modelBuilder.ApplyConfiguration(new AreaConfiguration(TenantProvider));
        modelBuilder.ApplyConfiguration(new TableConfiguration(TenantProvider));
        modelBuilder.ApplyConfiguration(new ReservationConfiguration(TenantProvider));
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_databaseOptions.Value.Database,
            x => x.MigrationsAssembly("TableGenius.Api.Repo.Database"));
    }
}