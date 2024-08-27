using HotelBookingManagement.Infrastructure;

namespace HotelBookingManagement.APIs;

public class RoomsService : RoomsServiceBase
{
    public RoomsService(HotelBookingManagementDbContext context)
        : base(context) { }
}
