using System;
using Microsoft.EntityFrameworkCore;
using TableGenius.Api.Entities.Default;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Repo.Database.Repositories;

public class IndependentBaseRepository<T>(RepositoryContext dataContext)
    : BaseRepository<T>(dataContext, null), IIndependentBaseRepository<T>
    where T : Base
{
    public override void Add(T entity)
    {
        entity.CreateDate = DateTime.Now;
        entity.ModDate = DateTime.Now;
        DbSet.Add(entity);
    }

    public override void Update(T entity)
    {
        entity.ModDate = DateTime.Now;
        DbSet.Attach(entity);
        DataContext.Entry(entity).State = EntityState.Modified;
        DataContext.Entry(entity).Property(x => x.Deleted).IsModified = false;
        DataContext.Entry(entity).Property(x => x.CreateDate).IsModified = false;
    }
}