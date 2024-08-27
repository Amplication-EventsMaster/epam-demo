using HotelBookingManagement.APIs;
using HotelBookingManagement.APIs.Common;
using HotelBookingManagement.APIs.Dtos;
using HotelBookingManagement.APIs.Errors;
using HotelBookingManagement.APIs.Extensions;
using HotelBookingManagement.Infrastructure;
using HotelBookingManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingManagement.APIs;

public abstract class RoomsServiceBase : IRoomsService
{
    protected readonly HotelBookingManagementDbContext _context;

    public RoomsServiceBase(HotelBookingManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Room
    /// </summary>
    public async Task<Room> CreateRoom(RoomCreateInput createDto)
    {
        var room = new RoomDbModel
        {
            CreatedAt = createDto.CreatedAt,
            NumberField = createDto.NumberField,
            Price = createDto.Price,
            TypeField = createDto.TypeField,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            room.Id = createDto.Id;
        }
        if (createDto.Hotel != null)
        {
            room.Hotel = await _context
                .Hotels.Where(hotel => createDto.Hotel.Id == hotel.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Reservations != null)
        {
            room.Reservations = await _context
                .Reservations.Where(reservation =>
                    createDto.Reservations.Select(t => t.Id).Contains(reservation.Id)
                )
                .ToListAsync();
        }

        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<RoomDbModel>(room.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Room
    /// </summary>
    public async Task DeleteRoom(RoomWhereUniqueInput uniqueId)
    {
        var room = await _context.Rooms.FindAsync(uniqueId.Id);
        if (room == null)
        {
            throw new NotFoundException();
        }

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Rooms
    /// </summary>
    public async Task<List<Room>> Rooms(RoomFindManyArgs findManyArgs)
    {
        var rooms = await _context
            .Rooms.Include(x => x.Hotel)
            .Include(x => x.Reservations)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return rooms.ConvertAll(room => room.ToDto());
    }

    /// <summary>
    /// Meta data about Room records
    /// </summary>
    public async Task<MetadataDto> RoomsMeta(RoomFindManyArgs findManyArgs)
    {
        var count = await _context.Rooms.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Room
    /// </summary>
    public async Task<Room> Room(RoomWhereUniqueInput uniqueId)
    {
        var rooms = await this.Rooms(
            new RoomFindManyArgs { Where = new RoomWhereInput { Id = uniqueId.Id } }
        );
        var room = rooms.FirstOrDefault();
        if (room == null)
        {
            throw new NotFoundException();
        }

        return room;
    }

    /// <summary>
    /// Update one Room
    /// </summary>
    public async Task UpdateRoom(RoomWhereUniqueInput uniqueId, RoomUpdateInput updateDto)
    {
        var room = updateDto.ToModel(uniqueId);

        if (updateDto.Hotel != null)
        {
            room.Hotel = await _context
                .Hotels.Where(hotel => updateDto.Hotel == hotel.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Reservations != null)
        {
            room.Reservations = await _context
                .Reservations.Where(reservation =>
                    updateDto.Reservations.Select(t => t).Contains(reservation.Id)
                )
                .ToListAsync();
        }

        _context.Entry(room).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Rooms.Any(e => e.Id == room.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a Hotel record for Room
    /// </summary>
    public async Task<Hotel> GetHotel(RoomWhereUniqueInput uniqueId)
    {
        var room = await _context
            .Rooms.Where(room => room.Id == uniqueId.Id)
            .Include(room => room.Hotel)
            .FirstOrDefaultAsync();
        if (room == null)
        {
            throw new NotFoundException();
        }
        return room.Hotel.ToDto();
    }

    /// <summary>
    /// Connect multiple Reservations records to Room
    /// </summary>
    public async Task ConnectReservations(
        RoomWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Rooms.Include(x => x.Reservations)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reservations.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Reservations);

        foreach (var child in childrenToConnect)
        {
            parent.Reservations.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Reservations records from Room
    /// </summary>
    public async Task DisconnectReservations(
        RoomWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Rooms.Include(x => x.Reservations)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reservations.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Reservations?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Reservations records for Room
    /// </summary>
    public async Task<List<Reservation>> FindReservations(
        RoomWhereUniqueInput uniqueId,
        ReservationFindManyArgs roomFindManyArgs
    )
    {
        var reservations = await _context
            .Reservations.Where(m => m.RoomId == uniqueId.Id)
            .ApplyWhere(roomFindManyArgs.Where)
            .ApplySkip(roomFindManyArgs.Skip)
            .ApplyTake(roomFindManyArgs.Take)
            .ApplyOrderBy(roomFindManyArgs.SortBy)
            .ToListAsync();

        return reservations.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Reservations records for Room
    /// </summary>
    public async Task UpdateReservations(
        RoomWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] childrenIds
    )
    {
        var room = await _context
            .Rooms.Include(t => t.Reservations)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (room == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reservations.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        room.Reservations = children;
        await _context.SaveChangesAsync();
    }
}
