using TableGenius.Api.Entities.Project;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Services.Queries;

public class ProjectExpertQueries : DatabaseBaseQueries<ProjectExpert>
{
    public ProjectExpertQueries(IDatabaseUnitOfWork unitOfWork) : base(unitOfWork)
    {
        BaseRepository = unitOfWork.ProjectExpertRepository();
    }
}