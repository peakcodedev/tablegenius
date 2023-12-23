using Autofac;
using TableGenius.Api.Repo.BlobStorage.Interfaces;

namespace TableGenius.Api.Repo.BlobStorage;

public static class BlobRepositoryInjector
{
    public static void RegisterModule(ContainerBuilder container)
    {
        container.RegisterType<BlobStorageApi>().As<IBlobStorageApi>();
    }
}