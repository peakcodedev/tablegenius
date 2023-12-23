using Autofac;
using TableGenius.Api.Services.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;
using TableGenius.Api.Services.Services;

namespace TableGenius.Api.Services;

public static class ServicesInjector
{
    public static void RegisterModule(ContainerBuilder container)
    {
        container.RegisterType<ProjectService>().As<IProjectService>();
        container.RegisterType<ProjectRatingService>().As<IProjectRatingService>();
        container.RegisterType<ProjectExpertService>().As<IProjectExpertService>();
        container.RegisterType<ProjectBoothService>().As<IProjectBoothService>();
        container.RegisterType<CompanyService>().As<ICompanyService>();
        container.RegisterType<UserService>().As<IUserService>();
        container.RegisterType<ProfessionService>().As<IProfessionService>();
        container.RegisterType<MailService>().As<IMailService>();
        container.RegisterType<ApplicationMailService>().As<IApplicationMailService>();
        container.RegisterType<FileUploader>().As<IFileUploader>();
        container.RegisterType<ProjectCollaborationService>().As<IProjectCollaborationService>();
        container.RegisterType<EmployeeService>().As<IEmployeeService>();
        container.RegisterType<ProjectImageService>().As<IProjectImageService>();
        container.RegisterType<CourseService>().As<ICourseService>();
        container.RegisterType<ProjectUserRatingService>().As<IProjectUserRatingService>();
    }
}