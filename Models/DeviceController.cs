using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FieldManagementSystem.Models;

/// <summary>
/// Represents a device or controller assigned to a specific field.
/// </summary>
public class DeviceController
{
    /// <summary>
    /// Unique identifier for the device controller.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Type of the device (e.g., "Irrigation", "Sensor").
    /// </summary>
    [Required]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// ID of the field to which this controller belongs.
    /// </summary>
    public int FieldId { get; set; }

    /// <summary>
    /// Navigation property to the field.
    /// </summary>
    [ForeignKey(nameof(FieldId))]
    public Field Field { get; set; } = null!;
}
