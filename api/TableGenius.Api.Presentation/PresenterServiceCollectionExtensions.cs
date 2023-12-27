using Microsoft.Extensions.DependencyInjection;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.Presenter;

namespace TableGenius.Api.Presentation;

public static class PresenterServiceCollectionExtensions
{
    public static void AddPresenters(this IServiceCollection services)
    {
        services.AddScoped<ILocationPresenter, LocationPresenter>();
        services.AddScoped<IReservationPresenter, ReservationPresenter>();
        services.AddScoped<ITablePresenter, TablePresenter>();
        services.AddScoped<IAreaPresenter, AreaPresenter>();
        services.AddScoped<ILocationAssignmentPresenter, LocationAssignmentPresenter>();
        services.AddScoped<IAreaSlotPresenter, AreaSlotPresenter>();
    }
}