using HotelBookingManagement.APIs;
using HotelBookingManagement.APIs.Common;
using HotelBookingManagement.APIs.Dtos;
using HotelBookingManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class HotelsControllerBase : ControllerBase
{
    protected readonly IHotelsService _service;

    public HotelsControllerBase(IHotelsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Hotel
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Hotel>> CreateHotel(HotelCreateInput input)
    {
        var hotel = await _service.CreateHotel(input);

        return CreatedAtAction(nameof(Hotel), new { id = hotel.Id }, hotel);
    }

    /// <summary>
    /// Delete one Hotel
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteHotel([FromRoute()] HotelWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteHotel(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Hotels
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Hotel>>> Hotels([FromQuery()] HotelFindManyArgs filter)
    {
        return Ok(await _service.Hotels(filter));
    }

    /// <summary>
    /// Meta data about Hotel records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> HotelsMeta([FromQuery()] HotelFindManyArgs filter)
    {
        return Ok(await _service.HotelsMeta(filter));
    }

    /// <summary>
    /// Get one Hotel
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Hotel>> Hotel([FromRoute()] HotelWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Hotel(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Hotel
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateHotel(
        [FromRoute()] HotelWhereUniqueInput uniqueId,
        [FromQuery()] HotelUpdateInput hotelUpdateDto
    )
    {
        try
        {
            await _service.UpdateHotel(uniqueId, hotelUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Rooms records to Hotel
    /// </summary>
    [HttpPost("{Id}/rooms")]
    public async Task<ActionResult> ConnectRooms(
        [FromRoute()] HotelWhereUniqueInput uniqueId,
        [FromQuery()] RoomWhereUniqueInput[] roomsId
    )
    {
        try
        {
            await _service.ConnectRooms(uniqueId, roomsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Rooms records from Hotel
    /// </summary>
    [HttpDelete("{Id}/rooms")]
    public async Task<ActionResult> DisconnectRooms(
        [FromRoute()] HotelWhereUniqueInput uniqueId,
        [FromBody()] RoomWhereUniqueInput[] roomsId
    )
    {
        try
        {
            await _service.DisconnectRooms(uniqueId, roomsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Rooms records for Hotel
    /// </summary>
    [HttpGet("{Id}/rooms")]
    public async Task<ActionResult<List<Room>>> FindRooms(
        [FromRoute()] HotelWhereUniqueInput uniqueId,
        [FromQuery()] RoomFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindRooms(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Rooms records for Hotel
    /// </summary>
    [HttpPatch("{Id}/rooms")]
    public async Task<ActionResult> UpdateRooms(
        [FromRoute()] HotelWhereUniqueInput uniqueId,
        [FromBody()] RoomWhereUniqueInput[] roomsId
    )
    {
        try
        {
            await _service.UpdateRooms(uniqueId, roomsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
