using System.ComponentModel.DataAnnotations;

namespace FieldManagementSystem.DTOs;

/// <summary>
/// Data transfer object used for creating a new field.
/// </summary>
public class FieldDto
{
    /// <summary>
    /// Name of the field.
    /// </summary>
    [Required(ErrorMessage = "Field name is required.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// ID of the user who manages this field.
    /// </summary>
    [Required(ErrorMessage = "UserId is required.")]
    public int UserId { get; set; }
}