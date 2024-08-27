using Microsoft.AspNetCore.Mvc;

namespace HotelBookingManagement.APIs;

[ApiController()]
public class HotelsController : HotelsControllerBase
{
    public HotelsController(IHotelsService service)
        : base(service) { }
}
