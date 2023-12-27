using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TableGenius.Api.Entities.Default;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Repo.Database.Providers;

namespace TableGenius.Api.Repo.Database.Repositories;

public abstract class BaseRepository<T>(RepositoryContext dataContext, TenantProvider tenantProvider)
    : DbSetBase<T>(dataContext, tenantProvider), IBaseRepository<T>
    where T : Base
{
    public abstract void Add(T entity);

    public void Delete(T entity)
    {
        entity.Deleted = true;
        entity.ModDate = DateTime.Now;
        DbSet.Attach(entity);
        DataContext.Entry(entity).State = EntityState.Modified;
        DataContext.Entry(entity).Property(x => x.CreateDate).IsModified = false;
    }

    public void Delete(T entity, bool removeFromDb)
    {
        if (removeFromDb)
        {
            DbSet.Remove(entity);
            DataContext.Entry(entity).State = EntityState.Deleted;
        }
        else
        {
            Delete(entity);
        }
    }

    public void DeleteById(Guid id, bool removeFromDb)
    {
        var entity = GetById(id);
        Delete(entity, removeFromDb);
    }

    public void DeleteById(Guid id)
    {
        DeleteById(id, false);
    }

    public IQueryable<T> GetAllAsNoTracking()
    {
        return DbSet.Where(o => !o.Deleted).AsNoTracking();
    }

    public IQueryable<T> GetAllIncludingDeleted()
    {
        return DbSet;
    }

    public void Commit()
    {
        DataContext.SaveChanges();
    }

    public void Rollback()
    {
        DataContext.Database.CurrentTransaction!.Rollback();
    }

    public T GetById(Guid id)
    {
        return GetById(id, false);
    }

    public abstract void Update(T entity);

    public T GetByIdAsNoTracking(Guid id)
    {
        return GetById(id, true);
    }

    private T GetById(Guid id, bool asNoTracking)
    {
        if (string.IsNullOrWhiteSpace(id.ToString())) return default;
        var primaryKey = DataContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties;
        var parameter = Expression.Parameter(typeof(T), "e");
        var predicate = Expression.Lambda<Func<T, bool>>(
            Expression.Equal(Expression.PropertyOrField(parameter, primaryKey.Single().Name),
                Expression.Constant(id)), parameter);

        var query = DbSet.AsQueryable();
        if (asNoTracking) query = query.AsNoTracking();
        var entity = query.FirstOrDefault(predicate);
        return !entity.Deleted ? entity : null;
    }
}