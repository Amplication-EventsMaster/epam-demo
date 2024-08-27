using HotelBookingManagement.APIs.Common;
using HotelBookingManagement.APIs.Dtos;

namespace HotelBookingManagement.APIs;

public interface IHotelsService
{
    /// <summary>
    /// Create one Hotel
    /// </summary>
    public Task<Hotel> CreateHotel(HotelCreateInput hotel);

    /// <summary>
    /// Delete one Hotel
    /// </summary>
    public Task DeleteHotel(HotelWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Hotels
    /// </summary>
    public Task<List<Hotel>> Hotels(HotelFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Hotel records
    /// </summary>
    public Task<MetadataDto> HotelsMeta(HotelFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Hotel
    /// </summary>
    public Task<Hotel> Hotel(HotelWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Hotel
    /// </summary>
    public Task UpdateHotel(HotelWhereUniqueInput uniqueId, HotelUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Rooms records to Hotel
    /// </summary>
    public Task ConnectRooms(HotelWhereUniqueInput uniqueId, RoomWhereUniqueInput[] roomsId);

    /// <summary>
    /// Disconnect multiple Rooms records from Hotel
    /// </summary>
    public Task DisconnectRooms(HotelWhereUniqueInput uniqueId, RoomWhereUniqueInput[] roomsId);

    /// <summary>
    /// Find multiple Rooms records for Hotel
    /// </summary>
    public Task<List<Room>> FindRooms(
        HotelWhereUniqueInput uniqueId,
        RoomFindManyArgs RoomFindManyArgs
    );

    /// <summary>
    /// Update multiple Rooms records for Hotel
    /// </summary>
    public Task UpdateRooms(HotelWhereUniqueInput uniqueId, RoomWhereUniqueInput[] roomsId);
}
