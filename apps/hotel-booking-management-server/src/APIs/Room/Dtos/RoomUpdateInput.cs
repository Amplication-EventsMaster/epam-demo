namespace HotelBookingManagement.APIs.Dtos;

public class RoomUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Hotel { get; set; }

    public string? Id { get; set; }

    public string? NumberField { get; set; }

    public double? Price { get; set; }

    public List<string>? Reservations { get; set; }

    public string? TypeField { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
