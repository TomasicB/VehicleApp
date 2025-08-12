using Vehicle.Models.DTOs;

namespace Vehicle.Models.Common;

public interface IVehicleOwner
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly DOB { get; set; }

    public IEnumerable<VehicleRegistrationDTO>? VehicleRegistrations { get; }
}
