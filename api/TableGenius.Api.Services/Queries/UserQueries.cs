using TableGenius.Api.Entities.Admin;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Services.Queries;

public class UserQueries : DatabaseBaseQueries<User>
{
    public UserQueries(IDatabaseUnitOfWork unitOfWork) : base(unitOfWork)
    {
        BaseRepository = unitOfWork.UserRepository();
    }
}