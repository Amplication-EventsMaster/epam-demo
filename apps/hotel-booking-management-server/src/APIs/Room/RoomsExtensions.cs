using HotelBookingManagement.APIs.Dtos;
using HotelBookingManagement.Infrastructure.Models;

namespace HotelBookingManagement.APIs.Extensions;

public static class RoomsExtensions
{
    public static Room ToDto(this RoomDbModel model)
    {
        return new Room
        {
            CreatedAt = model.CreatedAt,
            Hotel = model.HotelId,
            Id = model.Id,
            NumberField = model.NumberField,
            Price = model.Price,
            Reservations = model.Reservations?.Select(x => x.Id).ToList(),
            TypeField = model.TypeField,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static RoomDbModel ToModel(this RoomUpdateInput updateDto, RoomWhereUniqueInput uniqueId)
    {
        var room = new RoomDbModel
        {
            Id = uniqueId.Id,
            NumberField = updateDto.NumberField,
            Price = updateDto.Price,
            TypeField = updateDto.TypeField
        };

        if (updateDto.CreatedAt != null)
        {
            room.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Hotel != null)
        {
            room.HotelId = updateDto.Hotel;
        }
        if (updateDto.UpdatedAt != null)
        {
            room.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return room;
    }
}
