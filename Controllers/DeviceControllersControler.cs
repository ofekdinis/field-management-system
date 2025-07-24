using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FieldManagementSystem.Data;
using FieldManagementSystem.Models;
using FieldManagementSystem.DTOs;

namespace FieldManagementSystem.Controllers;

/// <summary>
/// The DeviceController is an API controller in an ASP.NET Core Web API project.
/// It is responsible for handling HTTP requests related to Device (or Controller) entities.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DeviceControllersController : ControllerBase
{
    private readonly AppDbContext _context;

    public DeviceControllersController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets a list of all deviceControllers with their related fields.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DeviceController>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DeviceController>>> GetControllers()
    {
        return await _context.DeviceControllers
            .Include(c => c.Field)
            .ToListAsync();
    }

    /// <summary>
    /// Gets a specific deviceController by ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DeviceController), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeviceController>> GetController(int id)
    {
        var deviceController = await _context.DeviceControllers
            .Include(c => c.Field)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (deviceController == null)
            return NotFound();

        return deviceController;
    }

    /// <summary>
    /// Creates a new deviceController.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(DeviceController), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DeviceController>> PostController(DeviceControllerDto dto)
    {
        var deviceController = new DeviceController
        {
            Type = dto.Type,
            FieldId = dto.FieldId
        };

        _context.DeviceControllers.Add(deviceController);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetController), new { id = deviceController.Id }, deviceController);
    }

    /// <summary>
    /// Updates an existing deviceController.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutController(int id, DeviceControllerDto dto)
    {
        var existing = await _context.DeviceControllers.FindAsync(id);
        if (existing == null)
            return NotFound();

        existing.Type = dto.Type;
        existing.FieldId = dto.FieldId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>
    /// Deletes a deviceController by ID.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteController(int id)
    {
        var controller = await _context.DeviceControllers.FindAsync(id);
        if (controller == null)
            return NotFound();

        _context.DeviceControllers.Remove(controller);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
