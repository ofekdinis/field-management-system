namespace FieldManagementSystem.Models;

public class DeviceController
{
    public int Id { get; set; }//id of a Controller/device
    public required string Type { get; set; }  // e.g., "Irrigation", "Sensor"
    public int FieldId { get; set; }//id of field that the controller is on
    public required Field Field { get; set; }//navigation property
}
