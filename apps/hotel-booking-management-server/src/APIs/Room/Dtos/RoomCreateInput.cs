namespace HotelBookingManagement.APIs.Dtos;

public class RoomCreateInput
{
    public DateTime CreatedAt { get; set; }

    public Hotel? Hotel { get; set; }

    public string? Id { get; set; }

    public string? NumberField { get; set; }

    public double? Price { get; set; }

    public List<Reservation>? Reservations { get; set; }

    public string? TypeField { get; set; }

    public DateTime UpdatedAt { get; set; }
}
