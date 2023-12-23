using TableGenius.Api.Entities.Project;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;
using TableGenius.Api.Services.Queries;

namespace TableGenius.Api.Services.Services;

public class ProjectRatingService : DatabaseServiceBase<ProjectRating>, IProjectRatingService
{
    public ProjectRatingService(IDatabaseUnitOfWorkFactory unitOfWorkFactory, IApplicationLogger logger) : base(logger)
    {
        DatabaseUnitOfWork = unitOfWorkFactory.Create();
        Querier = new ProjectRatingQueries(DatabaseUnitOfWork);
    }
}