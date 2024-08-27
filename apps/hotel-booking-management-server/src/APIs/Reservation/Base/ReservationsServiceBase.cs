using HotelBookingManagement.APIs;
using HotelBookingManagement.APIs.Common;
using HotelBookingManagement.APIs.Dtos;
using HotelBookingManagement.APIs.Errors;
using HotelBookingManagement.APIs.Extensions;
using HotelBookingManagement.Infrastructure;
using HotelBookingManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingManagement.APIs;

public abstract class ReservationsServiceBase : IReservationsService
{
    protected readonly HotelBookingManagementDbContext _context;

    public ReservationsServiceBase(HotelBookingManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Reservation
    /// </summary>
    public async Task<Reservation> CreateReservation(ReservationCreateInput createDto)
    {
        var reservation = new ReservationDbModel
        {
            CreatedAt = createDto.CreatedAt,
            EndDate = createDto.EndDate,
            StartDate = createDto.StartDate,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            reservation.Id = createDto.Id;
        }
        if (createDto.Customer != null)
        {
            reservation.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Room != null)
        {
            reservation.Room = await _context
                .Rooms.Where(room => createDto.Room.Id == room.Id)
                .FirstOrDefaultAsync();
        }

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ReservationDbModel>(reservation.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Reservation
    /// </summary>
    public async Task DeleteReservation(ReservationWhereUniqueInput uniqueId)
    {
        var reservation = await _context.Reservations.FindAsync(uniqueId.Id);
        if (reservation == null)
        {
            throw new NotFoundException();
        }

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Reservations
    /// </summary>
    public async Task<List<Reservation>> Reservations(ReservationFindManyArgs findManyArgs)
    {
        var reservations = await _context
            .Reservations.Include(x => x.Customer)
            .Include(x => x.Room)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return reservations.ConvertAll(reservation => reservation.ToDto());
    }

    /// <summary>
    /// Meta data about Reservation records
    /// </summary>
    public async Task<MetadataDto> ReservationsMeta(ReservationFindManyArgs findManyArgs)
    {
        var count = await _context.Reservations.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Reservation
    /// </summary>
    public async Task<Reservation> Reservation(ReservationWhereUniqueInput uniqueId)
    {
        var reservations = await this.Reservations(
            new ReservationFindManyArgs { Where = new ReservationWhereInput { Id = uniqueId.Id } }
        );
        var reservation = reservations.FirstOrDefault();
        if (reservation == null)
        {
            throw new NotFoundException();
        }

        return reservation;
    }

    /// <summary>
    /// Update one Reservation
    /// </summary>
    public async Task UpdateReservation(
        ReservationWhereUniqueInput uniqueId,
        ReservationUpdateInput updateDto
    )
    {
        var reservation = updateDto.ToModel(uniqueId);

        if (updateDto.Customer != null)
        {
            reservation.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Room != null)
        {
            reservation.Room = await _context
                .Rooms.Where(room => updateDto.Room == room.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(reservation).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Reservations.Any(e => e.Id == reservation.Id))
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
    /// Get a Customer record for Reservation
    /// </summary>
    public async Task<Customer> GetCustomer(ReservationWhereUniqueInput uniqueId)
    {
        var reservation = await _context
            .Reservations.Where(reservation => reservation.Id == uniqueId.Id)
            .Include(reservation => reservation.Customer)
            .FirstOrDefaultAsync();
        if (reservation == null)
        {
            throw new NotFoundException();
        }
        return reservation.Customer.ToDto();
    }

    /// <summary>
    /// Get a Room record for Reservation
    /// </summary>
    public async Task<Room> GetRoom(ReservationWhereUniqueInput uniqueId)
    {
        var reservation = await _context
            .Reservations.Where(reservation => reservation.Id == uniqueId.Id)
            .Include(reservation => reservation.Room)
            .FirstOrDefaultAsync();
        if (reservation == null)
        {
            throw new NotFoundException();
        }
        return reservation.Room.ToDto();
    }
}
