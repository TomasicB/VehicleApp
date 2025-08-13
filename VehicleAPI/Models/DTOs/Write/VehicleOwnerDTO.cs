using Vehicle.Models.Common.Write;

namespace Vehicle.Models.DTOs.Write;

public class VehicleOwnerWriteDTO : IVehicleOwnerWrite
{
    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateOnly DOB { get; set; }

    public IEnumerable<VehicleRegistrationDTO>? VehicleRegistrations { get; }
}
