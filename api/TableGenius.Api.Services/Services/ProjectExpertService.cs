using TableGenius.Api.Entities.Project;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;
using TableGenius.Api.Services.Queries;

namespace TableGenius.Api.Services.Services;

public class ProjectExpertService : DatabaseServiceBase<ProjectExpert>, IProjectExpertService
{
    public ProjectExpertService(IDatabaseUnitOfWorkFactory unitOfWorkFactory, IApplicationLogger logger) : base(logger)
    {
        DatabaseUnitOfWork = unitOfWorkFactory.Create();
        Querier = new ProjectExpertQueries(DatabaseUnitOfWork);
    }
}