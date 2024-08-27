using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingManagement.Infrastructure.Models;

[Table("Rooms")]
public class RoomDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? HotelId { get; set; }

    [ForeignKey(nameof(HotelId))]
    public HotelDbModel? Hotel { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? NumberField { get; set; }

    [Range(-999999999, 999999999)]
    public double? Price { get; set; }

    public List<ReservationDbModel>? Reservations { get; set; } = new List<ReservationDbModel>();

    [StringLength(1000)]
    public string? TypeField { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
