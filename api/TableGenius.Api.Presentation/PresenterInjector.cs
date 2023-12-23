using Autofac;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.Presenter;

namespace TableGenius.Api.Presentation;

public static class PresenterInjector
{
    public static void RegisterModule(ContainerBuilder container)
    {
        container.RegisterType<UserPresenter>().As<IUserPresenter>();
        container.RegisterType<CompanyPresenter>().As<ICompanyPresenter>();
        container.RegisterType<ProjectBoothPresenter>().As<IProjectBoothPresenter>();
        container.RegisterType<ProjectExpertPresenter>().As<IProjectExpertPresenter>();
        container.RegisterType<ProjectRatingPresenter>().As<IProjectRatingPresenter>();
        container.RegisterType<ProjectPresenter>().As<IProjectPresenter>();
        container.RegisterType<ProfessionPresenter>().As<IProfessionPresenter>();
        container.RegisterType<ProjectCollaborationPresenter>().As<IProjectCollaborationPresenter>();
        container.RegisterType<EmployeePresenter>().As<IEmployeePresenter>();
        container.RegisterType<StatsPresenter>().As<IStatsPresenter>();
        container.RegisterType<ProjectImagePresenter>().As<IProjectImagePresenter>();
        container.RegisterType<CoursePresenter>().As<ICoursePresenter>();
        container.RegisterType<ProjectUserRatingPresenter>().As<IProjectUserRatingPresenter>();
    }
}