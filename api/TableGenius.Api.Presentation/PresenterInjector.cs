using Autofac;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.Presenter;

namespace TableGenius.Api.Presentation;

public static class PresenterInjector
{
    public static void RegisterModule(ContainerBuilder container)
    {
        container.RegisterType<LocationPresenter>().As<ILocationPresenter>();
        container.RegisterType<ReservationPresenter>().As<IReservationPresenter>();
        container.RegisterType<TablePresenter>().As<ITablePresenter>();
        container.RegisterType<AreaPresenter>().As<IAreaPresenter>();
    }
}