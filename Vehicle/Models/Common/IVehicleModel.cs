namespace Vehicle.Models.Common;

public interface IVehicleModel
{
    public string Name { get; set; }

    public string Abrv { get; set; }

    public int VehicleMakeId { get; set; }
}
