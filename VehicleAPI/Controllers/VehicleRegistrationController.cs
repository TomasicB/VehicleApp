using Microsoft.AspNetCore.Mvc;
using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;
using Vehicle.Repository.Common;

namespace Vehicle.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleRegistrationController : ControllerBase
{
    private readonly IVehicleRegistrationRepository _regRepo;

    public VehicleRegistrationController(IVehicleRegistrationRepository regRepo)
    {
        _regRepo = regRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleRegistrationDTO>>> GetRegistrations()
    {
        try
        {
            var registration = await _regRepo.GetRegistrations();
            return Ok(registration);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("{number}")]
    public async Task<ActionResult<IEnumerable<VehicleRegistrationDTO>>> GetRegistrationById(string number)
    {
        try
        {
            var registration = await _regRepo.GetRegistrationByNumber(number);
            return Ok(registration);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPost]
    public async Task<ActionResult> InsRegistration([FromBody] VehicleRegistrationDTO r, int ModelId, int EngineId, int OwnerId)
    {
        if (r == null)
            return BadRequest("Registration is not entered.");

        if (ModelId == 0)
            return BadRequest("Model is not entered.");

        if (EngineId == 0)
            return BadRequest("Engine is not entered.");

        if (OwnerId == 0)
            return BadRequest("Owner is not entered.");

        try
        {
            await _regRepo.InsRegistration(r, ModelId, EngineId, OwnerId);
            return Ok(string.Format("Registration inserted. {0}", r.RegistrationNumber));
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DelRegistration(int id)
    {
        if (id == 0)
            return NotFound();

        try
        {
            await _regRepo.DelRegistration(id);
            return Ok("Registration is deleted");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdRegistration(int id, [FromBody] VehicleRegistrationWriteDTO UpdRegistration)
    {
        if (id == 0)
            return NotFound();

        try
        {
            await _regRepo.UpdRegistration(id, UpdRegistration);
            return Ok(string.Format("Registration data updated.\r\nNew Registration noumber {0}", UpdRegistration.RegistrationNumber));
        }
        catch (Exception)
        {
            throw;
        }
    }
}
