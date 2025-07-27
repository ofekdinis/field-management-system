using System.ComponentModel.DataAnnotations;

namespace FieldManagementSystem.DTOs;

/// <summary>
/// Data Transfer Object for creating or updating a device controller.
/// </summary>
public class DeviceControllerDto
{
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