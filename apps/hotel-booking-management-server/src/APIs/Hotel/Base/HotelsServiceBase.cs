using HotelBookingManagement.APIs;
using HotelBookingManagement.APIs.Common;
using HotelBookingManagement.APIs.Dtos;
using HotelBookingManagement.APIs.Errors;
using HotelBookingManagement.APIs.Extensions;
using HotelBookingManagement.Infrastructure;
using HotelBookingManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingManagement.APIs;

public abstract class HotelsServiceBase : IHotelsService
{
    protected readonly HotelBookingManagementDbContext _context;

    public HotelsServiceBase(HotelBookingManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Hotel
    /// </summary>
    public async Task<Hotel> CreateHotel(HotelCreateInput createDto)
    {
        var hotel = new HotelDbModel
        {
            Address = createDto.Address,
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            Rating = createDto.Rating,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            hotel.Id = createDto.Id;
        }
        if (createDto.Rooms != null)
        {
            hotel.Rooms = await _context
                .Rooms.Where(room => createDto.Rooms.Select(t => t.Id).Contains(room.Id))
                .ToListAsync();
        }

        _context.Hotels.Add(hotel);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<HotelDbModel>(hotel.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Hotel
    /// </summary>
    public async Task DeleteHotel(HotelWhereUniqueInput uniqueId)
    {
        var hotel = await _context.Hotels.FindAsync(uniqueId.Id);
        if (hotel == null)
        {
            throw new NotFoundException();
        }

        _context.Hotels.Remove(hotel);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Hotels
    /// </summary>
    public async Task<List<Hotel>> Hotels(HotelFindManyArgs findManyArgs)
    {
        var hotels = await _context
            .Hotels.Include(x => x.Rooms)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return hotels.ConvertAll(hotel => hotel.ToDto());
    }

    /// <summary>
    /// Meta data about Hotel records
    /// </summary>
    public async Task<MetadataDto> HotelsMeta(HotelFindManyArgs findManyArgs)
    {
        var count = await _context.Hotels.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Hotel
    /// </summary>
    public async Task<Hotel> Hotel(HotelWhereUniqueInput uniqueId)
    {
        var hotels = await this.Hotels(
            new HotelFindManyArgs { Where = new HotelWhereInput { Id = uniqueId.Id } }
        );
        var hotel = hotels.FirstOrDefault();
        if (hotel == null)
        {
            throw new NotFoundException();
        }

        return hotel;
    }

    /// <summary>
    /// Update one Hotel
    /// </summary>
    public async Task UpdateHotel(HotelWhereUniqueInput uniqueId, HotelUpdateInput updateDto)
    {
        var hotel = updateDto.ToModel(uniqueId);

        if (updateDto.Rooms != null)
        {
            hotel.Rooms = await _context
                .Rooms.Where(room => updateDto.Rooms.Select(t => t).Contains(room.Id))
                .ToListAsync();
        }

        _context.Entry(hotel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Hotels.Any(e => e.Id == hotel.Id))
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
    /// Connect multiple Rooms records to Hotel
    /// </summary>
    public async Task ConnectRooms(
        HotelWhereUniqueInput uniqueId,
        RoomWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Hotels.Include(x => x.Rooms)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Rooms.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Rooms);

        foreach (var child in childrenToConnect)
        {
            parent.Rooms.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Rooms records from Hotel
    /// </summary>
    public async Task DisconnectRooms(
        HotelWhereUniqueInput uniqueId,
        RoomWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Hotels.Include(x => x.Rooms)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Rooms.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Rooms?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Rooms records for Hotel
    /// </summary>
    public async Task<List<Room>> FindRooms(
        HotelWhereUniqueInput uniqueId,
        RoomFindManyArgs hotelFindManyArgs
    )
    {
        var rooms = await _context
            .Rooms.Where(m => m.HotelId == uniqueId.Id)
            .ApplyWhere(hotelFindManyArgs.Where)
            .ApplySkip(hotelFindManyArgs.Skip)
            .ApplyTake(hotelFindManyArgs.Take)
            .ApplyOrderBy(hotelFindManyArgs.SortBy)
            .ToListAsync();

        return rooms.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Rooms records for Hotel
    /// </summary>
    public async Task UpdateRooms(
        HotelWhereUniqueInput uniqueId,
        RoomWhereUniqueInput[] childrenIds
    )
    {
        var hotel = await _context
            .Hotels.Include(t => t.Rooms)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (hotel == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Rooms.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        hotel.Rooms = children;
        await _context.SaveChangesAsync();
    }
}
