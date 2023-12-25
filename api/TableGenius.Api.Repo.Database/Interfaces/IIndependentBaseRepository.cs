using TableGenius.Api.Entities.Default;

namespace TableGenius.Api.Repo.Database.Interfaces;

public interface IIndependentBaseRepository<T> : IBaseRepository<T> where T : Base
{
}