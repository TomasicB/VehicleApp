using Microsoft.AspNetCore.Mvc;
using Vehicle.Models.Common;
using Vehicle.Models.DTOs;
using Vehicle.Service.Common;

namespace Vehicle.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleOwnerController : ControllerBase
{
    private readonly IVehicleOwnerService _ownerService;

    public VehicleOwnerController(IVehicleOwnerService ownerRepo)
    {
        _ownerService = ownerRepo;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IVehicleOwner>>> GetOwners()
    {
        try
        {
            var owner = await _ownerService.GetOwners();
            return Ok(owner);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("byId")]
    public async Task<ActionResult<IEnumerable<IVehicleOwner>>> GetOwnerById(int id)
    {
        try
        {
            var owner = await _ownerService.GetOwnerById(id);
            return Ok(owner);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("byName")]
    public async Task<ActionResult<IEnumerable<IVehicleOwner>>> GetOwnerByName(string name)
    {
        try
        {
            var owner = await _ownerService.GetOwnerByName(name);
            return Ok(owner);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPost]
    public async Task<ActionResult> InsOwner([FromBody] VehicleOwnerDTO o)
    {
        if (o == null)
            return BadRequest("Owner is not entered.");

        try
        {
            await _ownerService.InsOwner(o);
            return Ok(string.Format("Owner inserted.\r\n{0} {1} {2}", o.FirstName, o.LastName, o.DOB));
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DelOwner(int id)
    {
        if (id == 0)
            return NotFound();

        try
        {
            await _ownerService.DelOwner(id);
            return Ok("Owner is deleted");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdOwner(int id, [FromBody] VehicleOwnerDTO UpdOwner)
    {
        if (id == 0)
            return NotFound();

        try
        {
            await _ownerService.UpdOwner(id, UpdOwner);
            return Ok(string.Format("Owner data updated.\r\n" +
                "New data: {0}\t{1}",
                UpdOwner.FirstName, UpdOwner.LastName));
        }
        catch (Exception)
        {
            throw;
        }
    }
}
