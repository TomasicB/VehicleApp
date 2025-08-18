using Microsoft.AspNetCore.Mvc;
//using System.Web.Mvc;
using Vehicle.Models.Common;
using Vehicle.Service.Common;

namespace Vehicle.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleEngineController : ControllerBase
{
    private readonly IVehicleEngineService _engineService;
   
    public VehicleEngineController(IVehicleEngineService engineService)
    {
        _engineService = engineService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IVehicleEngine>>> GetEnginesAsync()
    {
        try
        {
            var engine = await _engineService.GetEngineAsync();
            return Ok(engine);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("byId")]
    public async Task<ActionResult<IEnumerable<IVehicleEngine>>> GetEngineByIdAsync(int id)
    {
        try
        {
            var engine = await _engineService.GetEngineByIdAsync(id);
            return Ok(engine);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("byType")]
    public async Task<ActionResult<IEnumerable<IVehicleEngine>>> GetEngineByNameAsync(string type)
    {
        try
        {
            var engine = await _engineService.GetEngineByNameAsync(type);
            return Ok(engine);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
