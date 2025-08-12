using Microsoft.AspNetCore.Mvc;
using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;
using Vehicle.Repository.Common;

namespace Vehicle.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleMakeController : ControllerBase
{
    private readonly IVehicleMakeRepository _makeRepo;

    public VehicleMakeController(IVehicleMakeRepository makeRepo)
    {
        _makeRepo = makeRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleMakeDTO>>> GetMake()
    {
        try
        {
            var make = await _makeRepo.GetMake();
            return Ok(make);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<IEnumerable<VehicleMakeDTO>>> GetMakeByName(string name)
    {
        try
        { 
            var make = await _makeRepo.GetMakeByName(name);
            return Ok(make);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPost]
    public async Task<ActionResult> InsMake([FromBody] VehicleMakeDTO m)
    {
        if (m == null)
            return BadRequest("Make is not entered.");

        try
        {
            await _makeRepo.InsMake(m);
            return Ok(string.Format("Make inserted. {0}", m.Name));
        }
        catch (Exception) 
        {
            throw;
        }

    }

    [HttpDelete]
    public async Task<ActionResult> DelMake(int id)
    {
        if (id == 0)
            return NotFound();

        try
        {
            await _makeRepo.DelMake(id);
            return Ok("Make is deleted");
        }
        catch (Exception) 
        { 
            throw; 
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdMake(int id, [FromBody] VehicleMakeWriteDTO UpdMake)
    {
        if (id == 0)
            return NotFound();
        try
        {
            await _makeRepo.UpdMake(id, UpdMake);
            return Ok(string.Format("Make data updated.\r\nNew make name {0}", UpdMake.Name));
        }
        catch (Exception)
        {
            throw;
        }
    }
}
