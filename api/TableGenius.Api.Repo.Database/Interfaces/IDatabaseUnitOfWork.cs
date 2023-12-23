using System;
using TableGenius.Api.Entities.Admin;
using TableGenius.Api.Entities.Company;
using TableGenius.Api.Entities.Course;
using TableGenius.Api.Entities.General;
using TableGenius.Api.Entities.Project;

namespace TableGenius.Api.Repo.Database.Interfaces;

public interface IDatabaseUnitOfWork
{
    Guid UniqueId { get; set; }
    void Save();
    IDatabaseUnitOfWork GetCurrent();
    void RefreshContext();
    void Refresh<T>(T domainObj) where T : class;
    void Detach<T>(T domainObj) where T : class;
    void Rollback();

    IBaseRepository<ProjectRating> ProjectRatingRepository();
    IBaseRepository<Project> ProjectRepository();
    IBaseRepository<ProjectBooth> ProjectBoothRepository();
    IBaseRepository<ProjectExpert> ProjectExpertRepository();
    IBaseRepository<Company> CompanyRepository();
    IBaseRepository<User> UserRepository();
    IBaseRepository<Profession> ProfessionRepository();
    IBaseRepository<ProjectCollaboration> ProjectCollaborationRepository();
    IBaseRepository<Employee> EmployeeRepository();
    IBaseRepository<ProjectImage> ProjectImageRepository();
    IBaseRepository<Course> CourseRepository();
    IBaseRepository<ProjectUserRating> ProjectUserRatingRepository();
}