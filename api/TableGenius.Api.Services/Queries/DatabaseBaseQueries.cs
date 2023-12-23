using System;
using System.Linq;
using TableGenius.Api.Entities.Default;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Services.Queries;

public abstract class DatabaseBaseQueries<T> : IQueries<T> where T : Base
{
    private readonly IDatabaseUnitOfWork _unitOfWork;
    protected IBaseRepository<T> BaseRepository;

    protected DatabaseBaseQueries(IDatabaseUnitOfWork databaseUnitOfWork)
    {
        _unitOfWork = databaseUnitOfWork;
    }

    public void Add(T entity)
    {
        BaseRepository.Add(entity);
    }

    public void Delete(T entity, bool removeFromDatabase)
    {
        BaseRepository.Delete(entity, removeFromDatabase);
    }

    public void DeleteById(Guid id, bool removeFromDatabase)
    {
        BaseRepository.DeleteById(id, removeFromDatabase);
    }

    public IQueryable<T> GetAllAsNoTracking()
    {
        return BaseRepository.GetAllAsNoTracking();
    }

    public IQueryable<T> GetAllIncludingDeleted()
    {
        return BaseRepository.GetAllIncludingDeleted();
    }

    public T GetById(Guid id)
    {
        return BaseRepository.GetById(id);
    }

    public T GetByIdAsNoTracking(Guid id)
    {
        return BaseRepository.GetByIdAsNoTracking(id);
    }

    public void Update(T entity)
    {
        BaseRepository.Update(entity);
    }
}