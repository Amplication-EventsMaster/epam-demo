using HotelBookingManagement.Infrastructure;

namespace HotelBookingManagement.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(HotelBookingManagementDbContext context)
        : base(context) { }
}
