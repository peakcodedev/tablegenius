using TableGenius.Api.Entities.General;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;
using TableGenius.Api.Services.Queries;

namespace TableGenius.Api.Services.Services;

public class ProfessionService : DatabaseServiceBase<Profession>, IProfessionService
{
    public ProfessionService(IDatabaseUnitOfWorkFactory unitOfWorkFactory, IApplicationLogger logger) : base(logger)
    {
        DatabaseUnitOfWork = unitOfWorkFactory.Create();
        Querier = new ProfessionQueries(DatabaseUnitOfWork);
    }
}