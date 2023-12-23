using System;
using TableGenius.Api.Entities.Project;

namespace TableGenius.Api.Services.Interfaces.Database;

public interface IProjectCollaborationService : IDatabaseService<ProjectCollaboration>
{
    ProjectCollaboration GetByUserId(Guid userId);
}