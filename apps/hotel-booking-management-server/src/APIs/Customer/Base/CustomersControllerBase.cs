using HotelBookingManagement.APIs;
using HotelBookingManagement.APIs.Common;
using HotelBookingManagement.APIs.Dtos;
using HotelBookingManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CustomersControllerBase : ControllerBase
{
    protected readonly ICustomersService _service;

    public CustomersControllerBase(ICustomersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Customer>> CreateCustomer(CustomerCreateInput input)
    {
        var customer = await _service.CreateCustomer(input);

        return CreatedAtAction(nameof(Customer), new { id = customer.Id }, customer);
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteCustomer([FromRoute()] CustomerWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCustomer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Customer>>> Customers(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.Customers(filter));
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CustomersMeta(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.CustomersMeta(filter));
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Customer>> Customer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Customer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateCustomer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] CustomerUpdateInput customerUpdateDto
    )
    {
        try
        {
            await _service.UpdateCustomer(uniqueId, customerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Reservations records to Customer
    /// </summary>
    [HttpPost("{Id}/reservations")]
    public async Task<ActionResult> ConnectReservations(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] ReservationWhereUniqueInput[] reservationsId
    )
    {
        try
        {
            await _service.ConnectReservations(uniqueId, reservationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Reservations records from Customer
    /// </summary>
    [HttpDelete("{Id}/reservations")]
    public async Task<ActionResult> DisconnectReservations(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] ReservationWhereUniqueInput[] reservationsId
    )
    {
        try
        {
            await _service.DisconnectReservations(uniqueId, reservationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Reservations records for Customer
    /// </summary>
    [HttpGet("{Id}/reservations")]
    public async Task<ActionResult<List<Reservation>>> FindReservations(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] ReservationFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindReservations(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Reservations records for Customer
    /// </summary>
    [HttpPatch("{Id}/reservations")]
    public async Task<ActionResult> UpdateReservations(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] ReservationWhereUniqueInput[] reservationsId
    )
    {
        try
        {
            await _service.UpdateReservations(uniqueId, reservationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
