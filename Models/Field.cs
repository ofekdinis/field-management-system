using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FieldManagementSystem.Models;

/// <summary>
/// Represents a plot of land managed by a user.
/// </summary>
public class Field
{
    /// <summary>
    /// Unique identifier for the field.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Name of the field.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// ID of the user who manages the field.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// The user who manages this field.
    /// </summary>
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    /// <summary>
    /// The collection of device controllers associated with this field.
    /// </summary>
    public ICollection<DeviceController>? DeviceControllers { get; set; } = new List<DeviceController>();
}
