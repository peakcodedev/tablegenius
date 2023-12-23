using System;
using System.Collections.Generic;
using System.Linq;
using TableGenius.Api.Entities.Default;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Services.Services;

public abstract class DatabaseServiceBase<T> : IDatabaseService<T> where T : Base
{
    private readonly IApplicationLogger _logger;
    protected IBaseRepository<T> _repository;

    protected DatabaseServiceBase(IApplicationLogger logger)
    {
        _logger = logger;
    }

    public T Add(T entity, bool saveToDb)
    {
        try
        {
            _repository.Add(entity);
            if (saveToDb)
            {
                _repository.Commit();
                _logger.LogInformation<T>("Added (DatabaseServiceBase)");
                return entity;
            }
        }
        catch (Exception e)
        {
            _logger.LogError<T>(e, "Error adding (DatabaseServiceBase)");
        }

        return null;
    }

    public T Add(T entity)
    {
        return Add(entity, true);
    }

    public void Delete(T entity, bool saveToDb, bool removeFromDb)
    {
        try
        {
            _repository.Delete(entity, removeFromDb);
            if (saveToDb)
            {
                _repository.Commit();
                _logger.LogInformation<T>("Deleted (DatabaseServiceBase)");
            }
        }
        catch (Exception e)
        {
            _logger.LogError<T>(e, "Error on deleting (DatabaseServiceBase)");
        }
    }

    public void Delete(T entity, bool removeFromDb = false)
    {
        Delete(entity, true, removeFromDb);
    }

    public bool DeleteById(Guid id, bool saveToDb, bool removeFromDb = false)
    {
        try
        {
            _repository.DeleteById(id, removeFromDb);
            if (saveToDb)
            {
                _repository.Commit();
                _logger.LogInformation<T>("Deleted By Guid (DatabaseServiceBase)");
            }
        }
        catch (Exception e)
        {
            _repository.Rollback();
            _logger.LogError<T>(e, "Error on deleting by guid (DatabaseServiceBase)");
            return false;
        }

        return true;
    }

    public bool DeleteById(Guid id, bool removeFromDb = false)
    {
        return DeleteById(id, true, removeFromDb);
    }

    public List<T> GetAllAsNoTracking()
    {
        return GetAllQueryableAsNoTracking().ToList();
    }

    public List<T> GetAllIncludingDeleted()
    {
        return GetAllIncludingDeletedQueryable().ToList();
    }

    public IQueryable<T> GetAllQueryableAsNoTracking()
    {
        try
        {
            return _repository.GetAllAsNoTracking();
        }
        catch (Exception e)
        {
            _logger.LogError<T>(e, "error getting all queryable from database");
            return Enumerable.Empty<T>().AsQueryable();
        }
    }

    public T GetById(Guid id)
    {
        try
        {
            return _repository.GetById(id);
        }
        catch (Exception e)
        {
            _logger.LogError<T>(e, "error on getting by guid");
            return null;
        }
    }

    public T Update(T entity, bool saveToDb)
    {
        try
        {
            _repository.Update(entity);
            if (saveToDb) _repository.Commit();
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError<T>(e, "error on updating");
        }

        return null;
    }

    public T Update(T entity)
    {
        return Update(entity, true);
    }

    public T GetByIdAsNoTracking(Guid id)
    {
        try
        {
            return _repository.GetByIdAsNoTracking(id);
        }
        catch (Exception e)
        {
            _logger.LogError<T>(e, "error on getting by guid");
            return null;
        }
    }

    private IQueryable<T> GetAllIncludingDeletedQueryable()
    {
        try
        {
            return _repository.GetAllIncludingDeleted();
        }
        catch (Exception e)
        {
            _logger.LogError<T>(e, "error getting all queryable including deleted from database");
            return Enumerable.Empty<T>().AsQueryable();
        }
    }
}