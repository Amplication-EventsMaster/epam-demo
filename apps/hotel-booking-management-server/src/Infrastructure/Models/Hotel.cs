using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingManagement.Infrastructure.Models;

[Table("Hotels")]
public class HotelDbModel
{
    [StringLength(1000)]
    public string? Address { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [Range(-999999999, 999999999)]
    public double? Rating { get; set; }

    public List<RoomDbModel>? Rooms { get; set; } = new List<RoomDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
