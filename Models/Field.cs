using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FieldManagementSystem.Models;

public class Field
{
    public int Id { get; set; }//unique id
    public required string Name { get; set; }//name of field
    public int UserId { get; set; }//the user that control the field
    public required User User { get; set; }////navigation property
    public required ICollection<DeviceController> DeviceControllers { get; set; }//can have many controllers/devices
}
