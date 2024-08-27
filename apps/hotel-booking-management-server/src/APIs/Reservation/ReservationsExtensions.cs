using HotelBookingManagement.APIs.Dtos;
using HotelBookingManagement.Infrastructure.Models;

namespace HotelBookingManagement.APIs.Extensions;

public static class ReservationsExtensions
{
    public static Reservation ToDto(this ReservationDbModel model)
    {
        return new Reservation
        {
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            EndDate = model.EndDate,
            Id = model.Id,
            Room = model.RoomId,
            StartDate = model.StartDate,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ReservationDbModel ToModel(
        this ReservationUpdateInput updateDto,
        ReservationWhereUniqueInput uniqueId
    )
    {
        var reservation = new ReservationDbModel
        {
            Id = uniqueId.Id,
            EndDate = updateDto.EndDate,
            StartDate = updateDto.StartDate
        };

        if (updateDto.CreatedAt != null)
        {
            reservation.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            reservation.CustomerId = updateDto.Customer;
        }
        if (updateDto.Room != null)
        {
            reservation.RoomId = updateDto.Room;
        }
        if (updateDto.UpdatedAt != null)
        {
            reservation.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return reservation;
    }
}
