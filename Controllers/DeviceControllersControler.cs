using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FieldManagementSystem.Data;
using FieldManagementSystem.Models;

namespace FieldManagementSystem.Controllers;

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
    [ProducesResponseType(typeof(IEnumerable<DeviceController>), 200)]
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
    [ProducesResponseType(typeof(DeviceController), 200)]
    [ProducesResponseType(404)]
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
    /// <param name="controller">The deviceController object to create.</param>
    /// <returns>The created deviceController.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(DeviceController), 201)]
    [ProducesResponseType(400)]
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
    /// <param name="controller">The updated deviceController object.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
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
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
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
