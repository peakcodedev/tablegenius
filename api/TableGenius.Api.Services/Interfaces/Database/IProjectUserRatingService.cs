using System;
using TableGenius.Api.Entities.Project;

namespace TableGenius.Api.Services.Interfaces.Database;

public interface IProjectUserRatingService : IDatabaseService<ProjectUserRating>
{
    ProjectUserRating GetByUserId(Guid userId);
}