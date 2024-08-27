using Microsoft.AspNetCore.Mvc;

namespace HotelBookingManagement.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
