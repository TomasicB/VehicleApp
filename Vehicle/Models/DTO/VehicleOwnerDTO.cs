using Vehicle.Models.Common;

namespace Vehicle.Models.DTO;

public class VehicleOwnerDTO : IVehicleOwner
{
    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateOnly DOB { get; set; }
}
