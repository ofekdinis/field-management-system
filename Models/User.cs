using System.ComponentModel.DataAnnotations;

namespace FieldManagementSystem.Models;

/// <summary>
/// Represents a user in the field management system.
/// </summary>
public class User
{
    /// <summary>
    /// Unique identifier for the user (primary key).
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Full name of the user.
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Phone number used for notifications and communication.
    /// </summary>
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Email address used for updates and communication.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Collection of fields managed by the user.
    /// </summary>
    public ICollection<Field> Fields { get; set; } = new List<Field>();
}
