using System;
using System.Linq;
using System.Linq.Expressions;
using TableGenius.Api.Entities.Default;
using TableGenius.Api.Repo.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TableGenius.Api.Repo.Database;

public class BaseRepository<T> : DbSetBase<T>, IBaseRepository<T> where T : Base
{
    public BaseRepository(RepositoryContext dataContext) : base(dataContext)
    {
    }

    public void Add(T entity)
    {
        entity.CreateDate = DateTime.Now;
        entity.ModDate = DateTime.Now;
        DbSet.Add(entity);
    }

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
        var all = DbSet.Include(DataContext.GetIncludePaths(typeof(T))).Where(o => !o.Deleted).AsNoTracking();
        return all;
    }

    public IQueryable<T> GetAllIncludingDeleted()
    {
        var all = DbSet;
        return all;
    }

    public T GetById(Guid id)
    {
        return GetById(id, false);
    }

    public void Update(T entity)
    {
        entity.ModDate = DateTime.Now;
        DbSet.Attach(entity);
        DataContext.Entry(entity).State = EntityState.Modified;
        DataContext.Entry(entity).Property(x => x.Deleted).IsModified = false;
        DataContext.Entry(entity).Property(x => x.CreateDate).IsModified = false;
    }

    public T GetByIdAsNoTracking(Guid id)
    {
        return GetById(id, true);
    }

    private T GetById(Guid id, bool asNoTracking)
    {
        if (!string.IsNullOrWhiteSpace(id.ToString()))
        {
            var primaryKey = DataContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties;
            var parameter = Expression.Parameter(typeof(T), "e");
            var predicate = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(Expression.PropertyOrField(parameter, primaryKey.Single().Name),
                    Expression.Constant(id)), parameter);

            var query = DbSet.AsQueryable();
            query = query.Include(DataContext.GetIncludePaths(typeof(T)));
            if (asNoTracking) query = query.AsNoTracking();
            var entity = query.FirstOrDefault(predicate);
            return !entity.Deleted ? entity : null;
        }

        return default;
    }
}