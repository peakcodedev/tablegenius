using System;
using TableGenius.Api.Entities.Default;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Repo.Database.Repositories;

public class IndependentBaseRepository<T>(RepositoryContext dataContext)
    : BaseRepository<T>(dataContext), IIndependentBaseRepository<T>
    where T : Base
{
    public override void Add(T entity)
    {
        entity.CreateDate = DateTime.Now;
        entity.ModDate = DateTime.Now;
        DbSet.Add(entity);
    }
}