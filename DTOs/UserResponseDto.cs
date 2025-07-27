using System.ComponentModel.DataAnnotations;
/// <summary>
/// Data transfer object used when returning a new user or users.
/// </summary>
public class UserResponseDto
{
    /// <summary>
    /// Unique identifier for the user (primary key).
    /// </summary>
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// Full name of the user.
    /// </summary>
    [Required(ErrorMessage = "Name is required.")]
    public required string Name { get; set; }

    /// <summary>
    /// Phone number used for notifications and communication.
    /// </summary>
    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Phone number format is invalid.")]
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Email address used for updates and communication.
    /// </summary>
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email format is invalid.")]
    public required string Email { get; set; }
}