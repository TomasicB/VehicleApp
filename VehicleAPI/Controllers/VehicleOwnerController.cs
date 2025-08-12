using Microsoft.AspNetCore.Mvc;
using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;
using Vehicle.Repository.Common;

namespace Vehicle.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleOwnerController : ControllerBase
{
    private readonly IVehicleOwnerRepository _ownerRepo;

    public VehicleOwnerController(IVehicleOwnerRepository ownerRepo)
    {
        _ownerRepo = ownerRepo;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleOwnerDTO>>> GetOwners()
    {
        try
        {
            var owner = await _ownerRepo.GetOwners();
            return Ok(owner);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<IEnumerable<VehicleOwnerDTO>>> GetOwnerById(string name)
    {
        try
        {
            var owner = await _ownerRepo.GetOwnerByName(name);
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
            await _ownerRepo.InsOwner(o);
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
            await _ownerRepo.DelOwner(id);
            return Ok("Owner is deleted");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdOwner(int id, [FromBody] VehicleOwnerWriteDTO UpdOwner)
    {
        if (id == 0)
            return NotFound();

        try
        {
            await _ownerRepo.UpdOwner(id, UpdOwner);
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
