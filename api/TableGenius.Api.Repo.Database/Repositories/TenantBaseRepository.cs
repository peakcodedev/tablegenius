using System;
using TableGenius.Api.Entities.Default;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Repo.Database.Repositories;

public class TenantBaseRepository<T>(RepositoryContext dataContext)
    : BaseRepository<T>(dataContext), IBaseRepository<T>
    where T : TenantBase
{
    public override void Add(T entity)
    {
        entity.CreateDate = DateTime.Now;
        entity.ModDate = DateTime.Now;
        entity.TenantId = DataContext.TenantProvider.GetTenantId();
        DbSet.Add(entity);
    }
}