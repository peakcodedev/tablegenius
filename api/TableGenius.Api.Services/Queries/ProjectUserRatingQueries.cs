using System;
using System.Linq;
using TableGenius.Api.Entities.Project;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Services.Queries;

public class ProjectUserRatingQueries : DatabaseBaseQueries<ProjectUserRating>
{
    public ProjectUserRatingQueries(IDatabaseUnitOfWork unitOfWork) : base(unitOfWork)
    {
        BaseRepository = unitOfWork.ProjectUserRatingRepository();
    }

    public ProjectUserRating GetByUserId(Guid userId)
    {
        return GetAllAsNoTracking().SingleOrDefault(pc => pc.UserId == userId);
    }
}