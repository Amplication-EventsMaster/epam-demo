using HotelBookingManagement.APIs.Common;
using HotelBookingManagement.APIs.Dtos;

namespace HotelBookingManagement.APIs;

public interface ICustomersService
{
    /// <summary>
    /// Create one Customer
    /// </summary>
    public Task<Customer> CreateCustomer(CustomerCreateInput customer);

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public Task DeleteCustomer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Customers
    /// </summary>
    public Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Customer
    /// </summary>
    public Task<Customer> Customer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Customer
    /// </summary>
    public Task UpdateCustomer(CustomerWhereUniqueInput uniqueId, CustomerUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Reservations records to Customer
    /// </summary>
    public Task ConnectReservations(
        CustomerWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] reservationsId
    );

    /// <summary>
    /// Disconnect multiple Reservations records from Customer
    /// </summary>
    public Task DisconnectReservations(
        CustomerWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] reservationsId
    );

    /// <summary>
    /// Find multiple Reservations records for Customer
    /// </summary>
    public Task<List<Reservation>> FindReservations(
        CustomerWhereUniqueInput uniqueId,
        ReservationFindManyArgs ReservationFindManyArgs
    );

    /// <summary>
    /// Update multiple Reservations records for Customer
    /// </summary>
    public Task UpdateReservations(
        CustomerWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] reservationsId
    );
}
