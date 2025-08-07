using Vehicle.DAL.Entities;
using Vehicle.Models.Common;

namespace Vehicle.Models.DTO;

public class VehicleOwnerDTO : IVehicleOwner
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateOnly DOB { get; set; }

    public IEnumerable<VehicleRegistration>? VehicleRegistrations { get; }
}
