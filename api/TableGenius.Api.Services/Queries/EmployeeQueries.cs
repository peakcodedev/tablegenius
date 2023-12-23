using System;
using System.Linq;
using TableGenius.Api.Entities.Company;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Services.Queries;

public class EmployeeQueries : DatabaseBaseQueries<Employee>
{
    public EmployeeQueries(IDatabaseUnitOfWork unitOfWork) : base(unitOfWork)
    {
        BaseRepository = unitOfWork.EmployeeRepository();
    }

    public Employee GetByUserId(Guid userId)
    {
        return GetAllAsNoTracking().SingleOrDefault(pc => pc.UserId == userId);
    }
}