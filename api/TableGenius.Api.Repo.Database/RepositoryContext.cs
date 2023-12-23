using System;
using TableGenius.Api.Repo.Database.Configurations;
using TableGenius.Api.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectExpertConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectRatingConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectBoothConfiguration());
        modelBuilder.ApplyConfiguration(new ProfessionConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectCollaborationConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectImageConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectUserRatingConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_databaseOptions.Value.Database, x => x.MigrationsAssembly("TableGenius.Api.Web"));
    }
}