using System;
using System.Linq;
using TableGenius.Api.Entities.Course;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Services.Queries;

public class CourseQueries : DatabaseBaseQueries<Course>
{
    public CourseQueries(IDatabaseUnitOfWork unitOfWork) : base(unitOfWork)
    {
        BaseRepository = unitOfWork.CourseRepository();
    }

    public Course GetByUserId(Guid userId)
    {
        return GetAllAsNoTracking().SingleOrDefault(pc => pc.UserId == userId);
    }
}