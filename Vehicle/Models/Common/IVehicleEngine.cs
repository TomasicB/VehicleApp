namespace Vehicle.Models.Common;

public interface IVehicleEngine
{
    public int Id { get; set; }

    public string Type { get; set; }

    public string Abrv { get; set; }
}
