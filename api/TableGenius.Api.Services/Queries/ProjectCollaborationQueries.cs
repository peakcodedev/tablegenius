using System;
using System.Linq;
using TableGenius.Api.Entities.Project;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Services.Queries;

public class ProjectCollaborationQueries : DatabaseBaseQueries<ProjectCollaboration>
{
    public ProjectCollaborationQueries(IDatabaseUnitOfWork unitOfWork) : base(unitOfWork)
    {
        BaseRepository = unitOfWork.ProjectCollaborationRepository();
    }

    public ProjectCollaboration GetByUserId(Guid userId)
    {
        return GetAllAsNoTracking().SingleOrDefault(pc => pc.UserId == userId);
    }
}