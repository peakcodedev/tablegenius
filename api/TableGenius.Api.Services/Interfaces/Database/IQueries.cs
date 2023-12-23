using System;
using System.Linq;

namespace TableGenius.Api.Services.Interfaces.Database;

public interface IQueries<T>
{
    IQueryable<T> GetAllAsNoTracking();
    IQueryable<T> GetAllIncludingDeleted();
    T GetById(Guid id);
    T GetByIdAsNoTracking(Guid id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity, bool removeFromDatabase);
    void DeleteById(Guid id, bool removeFromDatabase);
}