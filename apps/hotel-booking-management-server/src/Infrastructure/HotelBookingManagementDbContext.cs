using HotelBookingManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingManagement.Infrastructure;

public class HotelBookingManagementDbContext : DbContext
{
    public HotelBookingManagementDbContext(
        DbContextOptions<HotelBookingManagementDbContext> options
    )
        : base(options) { }

    public DbSet<HotelDbModel> Hotels { get; set; }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<RoomDbModel> Rooms { get; set; }

    public DbSet<ReservationDbModel> Reservations { get; set; }
}
