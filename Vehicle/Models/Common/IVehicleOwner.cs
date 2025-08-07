using Vehicle.DAL.Entities;

namespace Vehicle.Models.Common;

public interface IVehicleOwner
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly DOB { get; set; }

    public IEnumerable<VehicleRegistration>? VehicleRegistrations { get; }
}
