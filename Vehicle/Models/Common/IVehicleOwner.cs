namespace Vehicle.Models.Common;

public interface IVehicleOwner
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly DOB { get; set; }
}
