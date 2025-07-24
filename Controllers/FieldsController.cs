using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FieldManagementSystem.Data;
using FieldManagementSystem.Models;

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
    /// Gets a list of all fields including related user and controllers.
    /// </summary>
    /// <returns>List of all fields.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Field>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Field>>> GetFields()
    {
        return await _context.Fields
            .Include(f => f.User)
            .Include(f => f.DeviceControllers)
            .ToListAsync();
    }

    /// <summary>
    /// Gets a specific field by ID.
    /// </summary>
    /// <param name="id">The ID of the field to retrieve.</param>
    /// <returns>The field with the given ID.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Field), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Field>> GetField(int id)
    {
        var field = await _context.Fields
            .Include(f => f.User)
            .Include(f => f.DeviceControllers)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (field == null)
            return NotFound();

        return field;
    }

    /// <summary>
    /// Creates a new field.
    /// </summary>
    /// <param name="field">The field object to create.</param>
    /// <returns>The created field.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Field), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Field>> PostField(Field field)
    {
        _context.Fields.Add(field);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetField), new { id = field.Id }, field);
    }

    /// <summary>
    /// Updates an existing field.
    /// </summary>
    /// <param name="id">The ID of the field to update.</param>
    /// <param name="field">The updated field object.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutField(int id, Field field)
    {
        if (id != field.Id)
            return BadRequest();

        _context.Entry(field).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Fields.Any(f => f.Id == id))
                return NotFound();
            else
                throw;
        }

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
}
