using Microsoft.AspNetCore.Mvc;
using Vehicle.Models.Common;
using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;
using Vehicle.Service.Common;

namespace Vehicle.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleRegistrationController : ControllerBase
{
    private readonly IVehicleRegistrationService _regService;

    public VehicleRegistrationController(IVehicleRegistrationService regRepo)
    {
        _regService = regRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IVehicleRegistration>>> GetRegistrations()
    {
        try
        {
            var registration = await _regService.GetRegistrations();
            return Ok(registration);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<IEnumerable<IVehicleRegistration>>> GetRegistrationById(int id)
    {
        try
        {
            var registration = await _regService.GetRegistrationById(id);
            return Ok(registration);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("{number:string}")]
    public async Task<ActionResult<IEnumerable<IVehicleRegistration>>> GetRegistrationByNumber(string number)
    {
        try
        {
            var registration = await _regService.GetRegistrationByNumber(number);
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
            await _regService.InsRegistration(r, ModelId, EngineId, OwnerId);
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
            await _regService.DelRegistration(id);
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
            await _regService.UpdRegistration(id, UpdRegistration);
            return Ok(string.Format("Registration data updated.\r\nNew Registration noumber {0}", UpdRegistration.RegistrationNumber));
        }
        catch (Exception)
        {
            throw;
        }
    }
}
