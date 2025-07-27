namespace FieldManagementSystem.DTOs;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// DTO representing a device controller assigned to a field.
/// </summary>
public class DeviceControllerResponseDto
{
    /// <summary>
    /// Unique identifier for the DeviceController (primary key).
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
    [Required]
    public int FieldId { get; set; }
}
