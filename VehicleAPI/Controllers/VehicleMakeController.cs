using Microsoft.AspNetCore.Mvc;
using Vehicle.Models.Common;
using Vehicle.Models.DTOs;
using Vehicle.Service.Common;

namespace Vehicle.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleMakeController : ControllerBase
{
    private readonly IVehicleMakeService _makeService;

    public VehicleMakeController(IVehicleMakeService makeService)
    {
        _makeService = makeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IVehicleMake>>> GetMake()
    {
        try
        {
            var make = await _makeService.GetMake();
            return Ok(make);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<IEnumerable<IVehicleMake>>> GetMakeById(int id)
    {
        try
        {
            var make = await _makeService.GetMakeById(id);
            return Ok(make);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("{name:string}")]
    public async Task<ActionResult<IEnumerable<IVehicleMake>>> GetMakeByName(string name)
    {
        try
        { 
            var make = await _makeService.GetMakeByName(name);
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
            await _makeService.InsMake(m);
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
            await _makeService.DelMake(id);
            return Ok("Make is deleted");
        }
        catch (Exception) 
        { 
            throw; 
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdMake(int id, [FromBody] VehicleMakeDTO UpdMake)
    {
        if (id == 0)
            return NotFound();
        try
        {
            await _makeService.UpdMake(id, UpdMake);
            return Ok(string.Format("Make data updated.\r\nNew make name {0}", UpdMake.Name));
        }
        catch (Exception)
        {
            throw;
        }
    }
}
