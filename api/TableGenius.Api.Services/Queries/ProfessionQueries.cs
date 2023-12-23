using TableGenius.Api.Entities.General;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Services.Queries;

public class ProfessionQueries : DatabaseBaseQueries<Profession>
{
    public ProfessionQueries(IDatabaseUnitOfWork unitOfWork) : base(unitOfWork)
    {
        BaseRepository = unitOfWork.ProfessionRepository();
    }
}