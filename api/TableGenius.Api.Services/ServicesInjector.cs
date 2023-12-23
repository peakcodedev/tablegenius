using Autofac;
using TableGenius.Api.Services.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;
using TableGenius.Api.Services.Services;

namespace TableGenius.Api.Services;

public static class ServicesInjector
{
    public static void RegisterModule(ContainerBuilder container)
    {
        container.RegisterType<LocationService>().As<ILocationService>();
        container.RegisterType<MailService>().As<IMailService>();
        container.RegisterType<ApplicationMailService>().As<IApplicationMailService>();
        container.RegisterType<TableService>().As<ITableService>();
        container.RegisterType<AreaService>().As<IAreaService>();
        container.RegisterType<ReservationService>().As<IReservationService>();
    }
}