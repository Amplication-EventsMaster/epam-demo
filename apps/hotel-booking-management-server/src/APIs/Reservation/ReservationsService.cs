using HotelBookingManagement.Infrastructure;

namespace HotelBookingManagement.APIs;

public class ReservationsService : ReservationsServiceBase
{
    public ReservationsService(HotelBookingManagementDbContext context)
        : base(context) { }
}
