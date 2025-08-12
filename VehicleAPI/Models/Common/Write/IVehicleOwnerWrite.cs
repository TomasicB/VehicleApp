using Vehicle.Models.DTOs;

namespace Vehicle.Models.Common.Write;

public interface IVehicleOwnerWrite
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly DOB { get; set; }

    public IEnumerable<VehicleRegistrationDTO>? VehicleRegistrations { get; }
}
