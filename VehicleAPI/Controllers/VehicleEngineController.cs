using Microsoft.AspNetCore.Mvc;
using Vehicle.Models.DTOs;
using Vehicle.Repository.Common;

namespace Vehicle.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleEngineController : ControllerBase
{
    private readonly IVehicleEngineRepository _engineRepo;
   
    public VehicleEngineController(IVehicleEngineRepository engineRepo)
    {
        _engineRepo = engineRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleEngineDTO>>> GetEngine()
    {
        try
        {
            var engine = await _engineRepo.GetEngine();
            return Ok(engine);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("{type}")]
    public async Task<ActionResult<IEnumerable<VehicleEngineDTO>>> GetEngineByName(string type)
    {
        try
        {
            var engine = await _engineRepo.GetEngineByName(type);
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
            await _engineRepo.InsEngine(e);
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
            await _engineRepo.DelEngine(id);
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
            await _engineRepo.UpdEngine(id, UpdEngine);
            return Ok(string.Format("Engine data updated.\r\nNew Engine name {0}", UpdEngine.Type));
        }
        catch (Exception)
        {
            throw;
        }
    }
}
