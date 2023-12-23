using Autofac;
using TableGenius.Api.Entities.Admin;
using TableGenius.Api.Entities.Company;
using TableGenius.Api.Entities.Course;
using TableGenius.Api.Entities.General;
using TableGenius.Api.Entities.Project;
using TableGenius.Api.Services.Interfaces.Database;
using TableGenius.Api.Services.Queries;

namespace TableGenius.Api.Services;

public static class QueriesInjector
{
    public static void RegisterModule(ContainerBuilder container)
    {
        // Queries
        container.RegisterType<CompanyQueries>().As<IQueries<Company>>();
        container.RegisterType<ProjectQueries>().As<IQueries<Project>>();
        container.RegisterType<ProjectRatingQueries>().As<IQueries<ProjectRating>>();
        container.RegisterType<ProjectExpertQueries>().As<IQueries<ProjectExpert>>();
        container.RegisterType<UserQueries>().As<IQueries<User>>();
        container.RegisterType<ProjectBoothQueries>().As<IQueries<ProjectBooth>>();
        container.RegisterType<ProfessionQueries>().As<IQueries<Profession>>();
        container.RegisterType<ProjectCollaborationQueries>().As<IQueries<ProjectCollaboration>>();
        container.RegisterType<EmployeeQueries>().As<IQueries<Employee>>();
        container.RegisterType<ProjectImageQueries>().As<IQueries<ProjectImage>>();
        container.RegisterType<CourseQueries>().As<IQueries<Course>>();
        container.RegisterType<ProjectUserRatingQueries>().As<IQueries<ProjectUserRating>>();
    }
}