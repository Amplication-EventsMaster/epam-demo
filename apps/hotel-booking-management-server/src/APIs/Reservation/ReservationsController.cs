using Microsoft.AspNetCore.Mvc;

namespace HotelBookingManagement.APIs;

[ApiController()]
public class ReservationsController : ReservationsControllerBase
{
    public ReservationsController(IReservationsService service)
        : base(service) { }
}
