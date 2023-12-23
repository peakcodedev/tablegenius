using Autofac;
using TableGenius.Api.Infrastructure.Interfaces;

namespace TableGenius.Api.Infrastructure;

public static class InfrastructureInjector
{
    public static void RegisterModule(ContainerBuilder container)
    {
        container.RegisterType<ApplicationLogger>().As<IApplicationLogger>().SingleInstance();
    }
}