using System;
using TableGenius.Api.Entities.Project;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;
using TableGenius.Api.Services.Queries;

namespace TableGenius.Api.Services.Services;

public class ProjectUserRatingService : DatabaseServiceBase<ProjectUserRating>, IProjectUserRatingService
{
    public ProjectUserRatingService(IDatabaseUnitOfWorkFactory unitOfWorkFactory, IApplicationLogger logger) :
        base(logger)
    {
        DatabaseUnitOfWork = unitOfWorkFactory.Create();
        Querier = new ProjectUserRatingQueries(DatabaseUnitOfWork);
    }

    public ProjectUserRating GetByUserId(Guid userId)
    {
        return ((ProjectUserRatingQueries) Querier).GetByUserId(userId);
    }
}