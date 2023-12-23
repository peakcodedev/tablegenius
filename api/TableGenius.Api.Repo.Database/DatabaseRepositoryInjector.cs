using Autofac;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Repo.Database;

public static class DatabaseRepositoryInjector
{
    public static void RegisterModule(ContainerBuilder container)
    {
        container.RegisterType<DatabaseUnitOfWork>().As<IDatabaseUnitOfWork>().InstancePerLifetimeScope();
        container.RegisterType<DatabaseUnitOfWorkFactory>().As<IDatabaseUnitOfWorkFactory>();
    }
}