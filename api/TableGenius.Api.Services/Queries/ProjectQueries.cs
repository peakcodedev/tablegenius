using TableGenius.Api.Entities.Project;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Services.Queries;

public class ProjectQueries : DatabaseBaseQueries<Project>
{
    public ProjectQueries(IDatabaseUnitOfWork unitOfWork) : base(unitOfWork)
    {
        BaseRepository = unitOfWork.ProjectRepository();
    }
}