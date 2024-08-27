using HotelBookingManagement.APIs.Dtos;
using HotelBookingManagement.Infrastructure.Models;

namespace HotelBookingManagement.APIs.Extensions;

public static class HotelsExtensions
{
    public static Hotel ToDto(this HotelDbModel model)
    {
        return new Hotel
        {
            Address = model.Address,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Name = model.Name,
            Rating = model.Rating,
            Rooms = model.Rooms?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static HotelDbModel ToModel(
        this HotelUpdateInput updateDto,
        HotelWhereUniqueInput uniqueId
    )
    {
        var hotel = new HotelDbModel
        {
            Id = uniqueId.Id,
            Address = updateDto.Address,
            Name = updateDto.Name,
            Rating = updateDto.Rating
        };

        if (updateDto.CreatedAt != null)
        {
            hotel.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            hotel.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return hotel;
    }
}
