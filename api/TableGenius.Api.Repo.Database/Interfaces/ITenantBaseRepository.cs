using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Repo.Database.Interfaces;

public interface ITenantBaseRepository<T> : IBaseRepository<T> where T : TenantBase
{
    new void Add(T entity);
}