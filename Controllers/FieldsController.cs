using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FieldManagementSystem.Data;
using FieldManagementSystem.Models;
using FieldManagementSystem.DTOs;

namespace FieldManagementSystem.Controllers;

/// <summary>
/// The FieldsController is an API controller in an ASP.NET Core Web API project.
/// It is responsible for handling HTTP requests related to Field entities, such as:
/// - Viewing fields (GET)
/// - Creating a new field (POST)
/// - Updating a field (PUT)
/// - Deleting a field (DELETE)
///
/// This controller serves as the bridge between the client (e.g., frontend application)
/// and the database, using Entity Framework Core (via AppDbContext) to perform
/// database operations.
/// </summary>

[ApiController]
[Route("api/[controller]")]
public class FieldsController : ControllerBase
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor that injects the application's DbContext.
    /// </summary>
    public FieldsController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets a list of all fields.
    /// </summary>
    /// <returns>List of all fields.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FieldResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FieldResponseDto>>> GetFields()
    {
        var fields = await _context.Fields.ToListAsync();

        var dtoList = fields.Select(field => new FieldResponseDto
        {
            Id = field.Id,
            Name = field.Name,
            UserId = field.UserId
        }).ToList();

        return Ok(dtoList);
    }

    /// <summary>
    /// Gets a specific field by ID.
    /// </summary>
    /// <param name="id">The ID of the field to retrieve.</param>
    /// <returns>The field with the given ID.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(FieldResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FieldResponseDto>> GetField(int id)
    {
        var field = await _context.Fields.FirstOrDefaultAsync(f => f.Id == id);

        if (field == null)
            return NotFound();

        var dto = new FieldResponseDto
        {
            Id = field.Id,
            Name = field.Name,
            UserId = field.UserId
        };

        return Ok(dto);
    }

    /// <summary>
    /// Creates a new field.
    /// </summary>
    /// <param name="fieldDto">The field data to create.</param>
    /// <returns>The created field.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(FieldResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FieldResponseDto>> PostField(FieldDto fieldDto)
    {
        var user = await _context.Users.FindAsync(fieldDto.UserId);
        if (user == null)
            return NotFound($"User with ID {fieldDto.UserId} not found.");

        var field = new Field
        {
            Name = fieldDto.Name,
            UserId = fieldDto.UserId
        };

        _context.Fields.Add(field);
        await _context.SaveChangesAsync();

        var responseDto = new FieldResponseDto
        {
            Id = field.Id,
            Name = field.Name,
            UserId = field.UserId
        };

        return CreatedAtAction(nameof(GetField), new { id = field.Id }, responseDto);
    }




    /// <summary>
    /// Updates an existing field.
    /// </summary>
    /// <param name="id">The ID of the field to update.</param>
    /// <param name="fieldDto">The updated field data.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutField(int id, FieldDto fieldDto)
    {   //get the field we want to update
        var field = await _context.Fields.FindAsync(id);
        if (field == null)
            return NotFound($"Field with ID {id} not found.");
        //update values
        field.Name = fieldDto.Name;
        field.UserId = fieldDto.UserId;
        //save changes
        await _context.SaveChangesAsync();
        return NoContent();
    }


    /// <summary>
    /// Deletes a field by ID.
    /// </summary>
    /// <param name="id">The ID of the field to delete.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteField(int id)
    {
        var field = await _context.Fields.FindAsync(id);
        if (field == null)
            return NotFound();

        _context.Fields.Remove(field);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    /// <summary>
    /// Gets all device controllers associated with a specific field.
    /// </summary>
    /// <param name="id">The ID of the field.</param>
    /// <returns>List of device controllers related to the field.</returns>
    /// <response code="200">Returns the list of device controllers</response>
    /// <response code="404">Field not found</response>
    [HttpGet("{id}/DeviceControllers")]
    [ProducesResponseType(typeof(IEnumerable<DeviceControllerResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<DeviceControllerResponseDto>>> GetDeviceControllersForField(int id)
    {
        var field = await _context.Fields
            .Include(f => f.DeviceControllers)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (field == null)
            return NotFound($"Field with ID {id} not found.");
        //if its not null take the values ,else return empty list.
        var deviceControllers = field.DeviceControllers ?? new List<DeviceController>();

        var responseDtos = deviceControllers.Select(dc => new DeviceControllerResponseDto
        {
            Id = dc.Id,
            Type = dc.Type,
            FieldId = dc.FieldId
        }).ToList();

        return Ok(responseDtos);
    }

}
