using HotelBookingManagement.Infrastructure;

namespace HotelBookingManagement.APIs;

public class HotelsService : HotelsServiceBase
{
    public HotelsService(HotelBookingManagementDbContext context)
        : base(context) { }
}
