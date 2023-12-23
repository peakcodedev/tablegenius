using System;
using TableGenius.Api.Entities.Course;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;
using TableGenius.Api.Services.Queries;

namespace TableGenius.Api.Services.Services;

public class CourseService : DatabaseServiceBase<Course>, ICourseService
{
    public CourseService(IDatabaseUnitOfWorkFactory unitOfWorkFactory, IApplicationLogger logger) :
        base(logger)
    {
        DatabaseUnitOfWork = unitOfWorkFactory.Create();
        Querier = new CourseQueries(DatabaseUnitOfWork);
    }

    public Course GetByUserId(Guid userId)
    {
        return ((CourseQueries) Querier).GetByUserId(userId);
    }
}