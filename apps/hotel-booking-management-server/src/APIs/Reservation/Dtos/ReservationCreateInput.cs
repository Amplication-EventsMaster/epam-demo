namespace HotelBookingManagement.APIs.Dtos;

public class ReservationCreateInput
{
    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Id { get; set; }

    public Room? Room { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime UpdatedAt { get; set; }
}
