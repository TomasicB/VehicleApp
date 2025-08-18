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
    public async Task<ActionResult<IEnumerable<IVehicleMake>>> GetMakesAsync()
    {
        try
        {
            var make = await _makeService.GetMakeAsync();
            return Ok(make);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("byId")]
    public async Task<ActionResult<IEnumerable<IVehicleMake>>> GetMakeByIdAsync(int id)
    {
        try
        {
            var make = await _makeService.GetMakeByIdAsync(id);
            return Ok(make);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("byName")]
    public async Task<ActionResult<IEnumerable<IVehicleMake>>> GetMakesByNameAsync(string name)
    {
        try
        { 
            var make = await _makeService.GetMakesByNameAsync(name);
            return Ok(make);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPost]
    public async Task<ActionResult> InsertMakeAsync([FromBody] VehicleMakeDTO m)
    {
        if (m == null)
            throw new ApplicationException("Make is not entered.");

        try
        {
            await _makeService.InsertMakeAsync(m);
            return Ok(string.Format("Make inserted. {0}", m.Name));
        }
        catch (Exception) 
        {
            throw;
        }

    }

    [HttpDelete]
    public async Task<ActionResult> DeleteMakeAsync(int id)
    {
        if (id == 0)
            throw new ApplicationException("Make is not found");

        try
        {
            await _makeService.DeleteMakeAsync(id);
            return Ok("Make is deleted");
        }
        catch (Exception) 
        { 
            throw; 
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateMakeAsync(int id, [FromBody] VehicleMakeDTO UpdMake)
    {
        if (id == 0)
            throw new ApplicationException("Make is not found");
        try
        {
            await _makeService.UpdateMakeAsync(id, UpdMake);
            return Ok(string.Format("Make data updated.\r\nNew make name {0}", UpdMake.Name));
        }
        catch (Exception)
        {
            throw;
        }
    }
}
