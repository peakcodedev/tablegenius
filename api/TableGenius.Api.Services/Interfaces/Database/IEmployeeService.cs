using System;
using TableGenius.Api.Entities.Company;

namespace TableGenius.Api.Services.Interfaces.Database;

public interface IEmployeeService : IDatabaseService<Employee>
{
    Employee GetByUserId(Guid userId);
}