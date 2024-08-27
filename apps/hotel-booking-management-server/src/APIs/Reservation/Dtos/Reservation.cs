namespace HotelBookingManagement.APIs.Dtos;

public class Reservation
{
    public DateTime CreatedAt { get; set; }

    public string? Customer { get; set; }

    public DateTime? EndDate { get; set; }

    public string Id { get; set; }

    public string? Room { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime UpdatedAt { get; set; }
}
