using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<IEnumerable<IVehicleEngine>>> GetEngine()
    {
        try
        {
            var engine = await _engineService.GetEngine();
            return Ok(engine);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("byId")]
    public async Task<ActionResult<IEnumerable<IVehicleEngine>>> GetEngineById(int id)
    {
        try
        {
            var engine = await _engineService.GetEngineById(id);
            return Ok(engine);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("byType")]
    public async Task<ActionResult<IEnumerable<IVehicleEngine>>> GetEngineByName(string type)
    {
        try
        {
            var engine = await _engineService.GetEngineByName(type);
            return Ok(engine);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
