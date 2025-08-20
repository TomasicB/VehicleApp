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

    public VehicleOwnerController(IVehicleOwnerService ownerService)
    {
        _ownerService = ownerService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IVehicleOwner>>> GetOwnersAsync()
    {
        try
        {
            var owner = await _ownerService.GetOwnersAsync();
            return Ok(owner);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("byId")]
    public async Task<ActionResult<IEnumerable<IVehicleOwner>>> GetOwnerByIdAsync(int id)
    {
        try
        {
            var owner = await _ownerService.GetOwnerByIdAsync(id);
            return Ok(owner);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpGet("byName")]
    public async Task<ActionResult<IEnumerable<IVehicleOwner>>> GetOwnersByNameAsync(string name)
    {
        try
        {
            var owner = await _ownerService.GetOwnersByNameAsync(name);
            return Ok(owner);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpPost]
    public async Task<ActionResult> InsertOwnerAsync([FromBody] VehicleOwnerDTO o)
    {
        if (o == null)
            throw new ApplicationException("Owner is not entered.");

        try
        {
            await _ownerService.InsertOwnerAsync(o);
            return Ok(string.Format("Owner inserted.\r\n{0} {1} {2}", o.FirstName, o.LastName, o.DOB));
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteOwnerAsync(int id)
    {
        if (id == 0)
            throw new ApplicationException("Owner is not found");

        try
        {
            await _ownerService.DeleteOwnerAsync(id);
            return Ok("Owner is deleted");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateOwnerAsync(int id, [FromBody] VehicleOwnerDTO UpdOwner)
    {
        if (id == 0)
            throw new ApplicationException("Owner is not found");

        try
        {
            await _ownerService.UpdateOwnerAsync(id, UpdOwner);
            return Ok(string.Format("Owner data updated.\r\n" +
                "New data: {0}\t{1}",
                UpdOwner.FirstName, UpdOwner.LastName));
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }
}
