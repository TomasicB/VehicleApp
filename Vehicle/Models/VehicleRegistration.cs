using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Models;

public class VehicleRegistration
{
    public int Id { get; set; }
    
    public string RegistrationNumber { get; set; }

    public int VehicleModelId { get; set; }

    public int VehicleEngineId { get; set; }

    public int VehicleOwnerId { get; set; }
}
