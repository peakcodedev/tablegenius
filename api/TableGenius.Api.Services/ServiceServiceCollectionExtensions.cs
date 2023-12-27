using Microsoft.Extensions.DependencyInjection;
using TableGenius.Api.Services.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;
using TableGenius.Api.Services.Services;

namespace TableGenius.Api.Services;

public static class ServiceServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<IApplicationMailService, ApplicationMailService>();
        services.AddScoped<ITableService, TableService>();
        services.AddScoped<IAreaService, AreaService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<ILocationAssignmentService, LocationAssignmentService>();
        services.AddScoped<IAreaSlotService, AreaSlotService>();
        services.AddScoped<IReservationAssignmentService, ReservationAssignmentService>();
    }
}