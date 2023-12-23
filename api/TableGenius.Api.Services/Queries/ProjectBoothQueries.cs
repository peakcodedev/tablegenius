using TableGenius.Api.Entities.Project;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Services.Queries;

public class ProjectBoothQueries : DatabaseBaseQueries<ProjectBooth>
{
    public ProjectBoothQueries(IDatabaseUnitOfWork unitOfWork) : base(unitOfWork)
    {
        BaseRepository = unitOfWork.ProjectBoothRepository();
    }
}