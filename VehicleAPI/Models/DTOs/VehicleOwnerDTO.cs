using Vehicle.Models.Common;

namespace Vehicle.Models.DTOs;

public class VehicleOwnerDTO : IVehicleOwner
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateOnly DOB { get; set; }

    public IEnumerable<VehicleRegistrationDTO>? VehicleRegistrations { get; }
}
