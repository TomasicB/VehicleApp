namespace Vehicle.Models.Common;

public interface IVehicleModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Abrv { get; set; }

    public int VehicleMakeId { get; set; }
}
