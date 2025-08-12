using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FieldManagementSystem.Data;
using FieldManagementSystem.Models;
using FieldManagementSystem.DTOs;

namespace FieldManagementSystem.Controllers;

/// <summary>
/// The UsersController is an API controller in an ASP.NET Core Web API project.
/// It is responsible for handling HTTP requests related to User entities, such as:
/// - Viewing users (GET)
/// - Creating a new user (POST)
/// - Updating a user (PUT)
/// - Deleting a user (DELETE)
///
/// This controller serves as the bridge between the client (e.g., frontend application)
/// and the database, using Entity Framework Core (via AppDbContext) to perform
/// database operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    ///inject DbContext inorder to query Users table
    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves the list of all users from the database.
    /// </summary>
    /// <returns>A list of UserResponseDto objects.</returns>
    /// <response code="200">Returns the list of users</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();

        var dtoList = users.Select(user => new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email
        }).ToList();

        return Ok(dtoList);
    }

    /// <summary>
    /// Fetches a user by ID. Returns 404 if not found.
    /// </summary>
    /// <response code="200">Returns the user</response>
    /// <response code="404">User not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponseDto>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        var userDto = new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email
        };

        return Ok(userDto);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <response code="201">User created successfully</response>
    /// <response code="409">User with the same ID already exists</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserResponseDto>> PostUser(UserDto userDto)
    {
        var user = new User
        {
            Name = userDto.Name,
            PhoneNumber = userDto.PhoneNumber,
            Email = userDto.Email
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var resultDto = new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email
        };

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, resultDto);
    }


    /// <summary>Updates an existing user. Returns 404 if the user is not found.</summary>
    /// <response code="204">User updated successfully</response>
    /// <response code="400">Bad request (ID mismatch)</response>
    /// <response code="404">User not found</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutUser(int id, UserDto userDto)
    {
        var existingUser = await _context.Users.FindAsync(id);
        if (existingUser == null)
            return NotFound();

        // Update only allowed properties
        existingUser.Name = userDto.Name;
        existingUser.PhoneNumber = userDto.PhoneNumber;
        existingUser.Email = userDto.Email;

        // Save changes
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>Deletes a user by ID.</summary>
    /// <response code="204">User deleted successfully</response>
    /// <response code="404">User not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users
            .Include(u => u.Fields)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return NotFound();

        _context.Fields.RemoveRange(user.Fields);
        _context.Users.Remove(user);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Retrieves all fields related to a specific user by user ID.
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>List of FieldDto objects</returns>
    /// <response code="200">Returns the list of fields</response>
    /// <response code="404">User not found</response>
    [HttpGet("{id}/Fields")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<FieldResponseDto>>> GetFieldsForUser(int id)
    {
        var user = await _context.Users
            .Include(u => u.Fields)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return NotFound($"User with ID {id} not found.");

        var fieldDtos = (user.Fields ?? new List<Field>())
            .Select(field => new FieldResponseDto
            {
                Id=field.Id,
                Name = field.Name,
                UserId = field.UserId
            }).ToList();


        return Ok(fieldDtos);
    }

}
