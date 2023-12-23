using System;
using TableGenius.Api.Entities.Company;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;
using TableGenius.Api.Services.Queries;

namespace TableGenius.Api.Services.Services;

public class EmployeeService : DatabaseServiceBase<Employee>, IEmployeeService
{
    public EmployeeService(IDatabaseUnitOfWorkFactory unitOfWorkFactory, IApplicationLogger logger) :
        base(logger)
    {
        DatabaseUnitOfWork = unitOfWorkFactory.Create();
        Querier = new EmployeeQueries(DatabaseUnitOfWork);
    }

    public Employee GetByUserId(Guid userId)
    {
        return ((EmployeeQueries) Querier).GetByUserId(userId);
    }
}