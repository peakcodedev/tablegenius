using System;
using Microsoft.EntityFrameworkCore;
using TableGenius.Api.Entities.Default;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Repo.Database.Providers;

namespace TableGenius.Api.Repo.Database.Repositories;

public class TenantBaseRepository<T>(RepositoryContext dataContext, TenantProvider tenantProvider)
    : BaseRepository<T>(dataContext, tenantProvider), ITenantBaseRepository<T>
    where T : TenantBase
{
    public override void Add(T entity)
    {
        entity.CreateDate = DateTime.Now;
        entity.ModDate = DateTime.Now;
        entity.TenantId = TenantProvider.GetTenantId();
        DbSet.Add(entity);
    }

    public override void Update(T entity)
    {
        entity.ModDate = DateTime.Now;
        DbSet.Attach(entity);
        DataContext.Entry(entity).State = EntityState.Modified;
        DataContext.Entry(entity).Property(x => x.Deleted).IsModified = false;
        DataContext.Entry(entity).Property(x => x.CreateDate).IsModified = false;
        DataContext.Entry(entity).Property(x => x.TenantId).IsModified = false;
    }
}