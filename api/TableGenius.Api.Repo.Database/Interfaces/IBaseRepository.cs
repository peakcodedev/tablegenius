using System;
using System.Linq;
using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Repo.Database.Interfaces;

public interface IBaseRepository<T> : IDbSetBase<T> where T : Base
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Delete(T entity, bool removeFromDb);
    void DeleteById(Guid id, bool removeFromDb);
    void DeleteById(Guid id);
    T GetById(Guid id);
    T GetByIdAsNoTracking(Guid id);
    IQueryable<T> GetAllAsNoTracking();
    IQueryable<T> GetAllIncludingDeleted();
    void Commit();
    void Rollback();
}