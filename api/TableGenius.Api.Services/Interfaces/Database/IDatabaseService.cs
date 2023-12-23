using System;
using System.Collections.Generic;
using System.Linq;

namespace TableGenius.Api.Services.Interfaces.Database;

public interface IDatabaseService<T> where T : class
{
    T Add(T entity, bool saveToDb);
    T Add(T entity);

    T Update(T entity, bool saveToDb);
    T Update(T entity);

    void Delete(T entity, bool saveToDb, bool removeFromDb);
    void Delete(T entity, bool removeFromDb = false);

    bool DeleteById(Guid id, bool saveToDb, bool removeFromDb = false);
    bool DeleteById(Guid id, bool removeFromDb = false);

    T GetById(Guid id);
    T GetByIdAsNoTracking(Guid id);

    List<T> GetAllAsNoTracking();

    List<T> GetAllIncludingDeleted();

    IQueryable<T> GetAllQueryableAsNoTracking();
}