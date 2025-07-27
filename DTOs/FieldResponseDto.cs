using System.ComponentModel.DataAnnotations;

namespace FieldManagementSystem.DTOs;

/// <summary>
/// Represents the data returned for a field in API responses.
/// </summary>
public class FieldResponseDto
{
    /// <summary>
    /// Unique identifier for the field.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the field.
    /// </summary>
    [Required(ErrorMessage = "Field name is required.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// ID of the user who manages this field.
    /// </summary>
    public int UserId { get; set; }
}
