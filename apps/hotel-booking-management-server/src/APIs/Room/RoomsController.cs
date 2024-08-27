using Microsoft.AspNetCore.Mvc;

namespace HotelBookingManagement.APIs;

[ApiController()]
public class RoomsController : RoomsControllerBase
{
    public RoomsController(IRoomsService service)
        : base(service) { }
}
