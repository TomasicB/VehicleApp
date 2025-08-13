using Microsoft.AspNetCore.Mvc;
using Vehicle.Models.Common;
using Vehicle.Models.DTOs;
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

    [HttpGet("{id:int}")]
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

    [HttpGet("{type:string}")]
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

    [HttpPost]
    public async Task<ActionResult> InsEngine([FromBody] VehicleEngineDTO e)
    {
        if (e == null)
            return BadRequest("Engine is not entered.");
        
        try
        {
            await _engineService.InsEngine(e);
            return Ok(string.Format("Engine inserted. {0}", e.Type));
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DelEngine(int id)
    {
        if (id == 0)
            return NotFound();
        try
        {
            await _engineService.DelEngine(id);
            return Ok("Engine is deleted");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdEngine(int id, [FromBody] VehicleEngineDTO UpdEngine)
    {
        if (id == 0)
            return NotFound();
        try
        {
            await _engineService.UpdEngine(id, UpdEngine);
            return Ok(string.Format("Engine data updated.\r\nNew Engine name {0}", UpdEngine.Type));
        }
        catch (Exception)
        {
            throw;
        }
    }
}
