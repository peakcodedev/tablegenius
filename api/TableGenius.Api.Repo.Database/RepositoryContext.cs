using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TableGenius.Api.Repo.Database.Configurations;
using TableGenius.Api.Settings;

namespace TableGenius.Api.Repo.Database;

public class RepositoryContext : DbContext
{
    private readonly IOptions<DatabaseOptions> _databaseOptions;

    public RepositoryContext(IOptions<DatabaseOptions> options)
    {
        ContextId = Guid.NewGuid();
        _databaseOptions = options;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public Guid ContextId { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LocationConfiguration());
        modelBuilder.ApplyConfiguration(new AreaConfiguration());
        modelBuilder.ApplyConfiguration(new TableConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_databaseOptions.Value.Database,
            x => x.MigrationsAssembly("TableGenius.Api.Repo.Database"));
    }
}