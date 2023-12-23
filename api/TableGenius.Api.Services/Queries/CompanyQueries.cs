using TableGenius.Api.Entities.Company;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Services.Queries;

public class CompanyQueries : DatabaseBaseQueries<Company>
{
    public CompanyQueries(IDatabaseUnitOfWork unitOfWork) : base(unitOfWork)
    {
        BaseRepository = unitOfWork.CompanyRepository();
    }
}