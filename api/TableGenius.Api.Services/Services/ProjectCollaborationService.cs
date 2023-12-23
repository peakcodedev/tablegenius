using System;
using TableGenius.Api.Entities.Project;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;
using TableGenius.Api.Services.Queries;

namespace TableGenius.Api.Services.Services;

public class ProjectCollaborationService : DatabaseServiceBase<ProjectCollaboration>, IProjectCollaborationService
{
    public ProjectCollaborationService(IDatabaseUnitOfWorkFactory unitOfWorkFactory, IApplicationLogger logger) :
        base(logger)
    {
        DatabaseUnitOfWork = unitOfWorkFactory.Create();
        Querier = new ProjectCollaborationQueries(DatabaseUnitOfWork);
    }

    public ProjectCollaboration GetByUserId(Guid userId)
    {
        return ((ProjectCollaborationQueries) Querier).GetByUserId(userId);
    }
}