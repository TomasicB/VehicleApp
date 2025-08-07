namespace Vehicle.DAL.Entities;

public class VehicleOwner
{
    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateOnly DOB { get; set; }

    public IEnumerable<VehicleRegistration>? VehicleRegistrations { get; }
}
