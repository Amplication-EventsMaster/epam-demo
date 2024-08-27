using HotelBookingManagement.APIs;

namespace HotelBookingManagement;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomersService, CustomersService>();
        services.AddScoped<IHotelsService, HotelsService>();
        services.AddScoped<IReservationsService, ReservationsService>();
        services.AddScoped<IRoomsService, RoomsService>();
    }
}
