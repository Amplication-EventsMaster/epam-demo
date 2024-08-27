using HotelBookingManagement.APIs.Common;
using HotelBookingManagement.APIs.Dtos;

namespace HotelBookingManagement.APIs;

public interface IRoomsService
{
    /// <summary>
    /// Create one Room
    /// </summary>
    public Task<Room> CreateRoom(RoomCreateInput room);

    /// <summary>
    /// Delete one Room
    /// </summary>
    public Task DeleteRoom(RoomWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Rooms
    /// </summary>
    public Task<List<Room>> Rooms(RoomFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Room records
    /// </summary>
    public Task<MetadataDto> RoomsMeta(RoomFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Room
    /// </summary>
    public Task<Room> Room(RoomWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Room
    /// </summary>
    public Task UpdateRoom(RoomWhereUniqueInput uniqueId, RoomUpdateInput updateDto);

    /// <summary>
    /// Get a Hotel record for Room
    /// </summary>
    public Task<Hotel> GetHotel(RoomWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Reservations records to Room
    /// </summary>
    public Task ConnectReservations(
        RoomWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] reservationsId
    );

    /// <summary>
    /// Disconnect multiple Reservations records from Room
    /// </summary>
    public Task DisconnectReservations(
        RoomWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] reservationsId
    );

    /// <summary>
    /// Find multiple Reservations records for Room
    /// </summary>
    public Task<List<Reservation>> FindReservations(
        RoomWhereUniqueInput uniqueId,
        ReservationFindManyArgs ReservationFindManyArgs
    );

    /// <summary>
    /// Update multiple Reservations records for Room
    /// </summary>
    public Task UpdateReservations(
        RoomWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] reservationsId
    );
}
