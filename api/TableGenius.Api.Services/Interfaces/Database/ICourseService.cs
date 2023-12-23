using System;
using TableGenius.Api.Entities.Course;

namespace TableGenius.Api.Services.Interfaces.Database;

public interface ICourseService : IDatabaseService<Course>
{
    Course GetByUserId(Guid userId);
}