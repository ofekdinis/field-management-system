using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FieldManagementSystem.Data;
using FieldManagementSystem.Models;

namespace FieldManagementSystem.Controllers;
/// <summary>
/// The DeviceController is an API controller in an ASP.NET Core Web API project.
/// It is responsible for handling HTTP requests related to Device (or Controller) entities, such as:
/// - Viewing devices (GET)
/// - Creating a new device (POST)
/// - Updating a device (PUT)
/// - Deleting a device (DELETE)
///
/// This controller serves as the bridge between the client (e.g., frontend application)
/// and the database, using Entity Framework Core (via AppDbContext) to perform
/// database operations.
/// </summary>

[ApiController]
[Route("api/[controller]")]
public class DeviceControllersControler : ControllerBase
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor that injects the application's DbContext.
    /// </summary>
    public DeviceControllersControler(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets a list of all deviceController with their related field.
    /// </summary>
    /// <returns>List of all deviceController.</returns>
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
    /// <param name="id">The ID of the deviceController to retrieve.</param>
    /// <returns>The deviceController with the given ID.</returns>
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
    /// <param name="deviceController">The deviceController object to create.</param>
    /// <returns>The created deviceController.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(DeviceController), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DeviceController>> PostController(DeviceController deviceController)
    {
        _context.DeviceControllers.Add(deviceController);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetController), new { id = deviceController.Id }, deviceController);
    }

    /// <summary>
    /// Updates an existing deviceController.
    /// </summary>
    /// <param name="id">The ID of the deviceController to update.</param>
    /// <param name="deviceController">The updated deviceController object.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutController(int id, DeviceController deviceController)
    {
        if (id != deviceController.Id)
            return BadRequest();

        _context.Entry(deviceController).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.DeviceControllers.Any(c => c.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    /// <summary>
    /// Deletes a deviceController by ID.
    /// </summary>
    /// <param name="id">The ID of the controller to delete.</param>
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
