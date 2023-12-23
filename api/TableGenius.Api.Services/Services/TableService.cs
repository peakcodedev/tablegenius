using TableGenius.Api.Entities.Place;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Services.Services;

public class TableService : DatabaseServiceBase<Table>, ITableService
{
    public TableService(ITableRepository tableRepository, IApplicationLogger logger) :
        base(logger)
    {
        _repository = tableRepository;
    }
}