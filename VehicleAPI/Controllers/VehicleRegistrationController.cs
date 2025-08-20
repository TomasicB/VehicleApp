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

    public VehicleRegistrationController(IVehicleRegistrationService regService)
    {
        _regService = regService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IVehicleRegistration>>> GetRegistrationsAsync()
    {
        try
        {
            var registration = await _regService.GetRegistrationsAsync();
            return Ok(registration);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpGet("byId")]
    public async Task<ActionResult<IEnumerable<IVehicleRegistration>>> GetRegistrationByIdAsync(int id)
    {
        try
        {
            var registration = await _regService.GetRegistrationByIdAsync(id);
            return Ok(registration);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpGet("byNumber")]
    public async Task<ActionResult<IEnumerable<IVehicleRegistration>>> GetRegistrationByNumberAsync(string number)
    {
        try
        {
            var registration = await _regService.GetRegistrationByNumberAsync(number);
            return Ok(registration);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpGet("byEngine")]
    public async Task<ActionResult<IEnumerable<IVehicleRegistration>>> GetRegistrationsByEngineAsync(string engine)
    {
        try
        {
            var registration = await _regService.GetRegistrationsByEngineAsync(engine);
            return Ok(registration);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpGet("byModel")]
    public async Task<ActionResult<IEnumerable<IVehicleRegistration>>> GetRegistrationsByModelAsync(string model)
    {
        try
        {
            var registration = await _regService.GetRegistrationsByModelAsync(model);
            return Ok(registration);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpGet("byOwner")]
    public async Task<ActionResult<IEnumerable<IVehicleRegistration>>> GetRegistrationsByOwnerAsync(string owner)
    {
        try
        {
            var registration = await _regService.GetRegistrationsByOwnerAsync(owner);
            return Ok(registration);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpPost]
    public async Task<ActionResult> InsertRegistrationAsync([FromBody] VehicleRegistrationDTO r, int ModelId, int EngineId, int OwnerId)
    {
        if (r == null)
            throw new ApplicationException("Registration is not entered.");

        if (ModelId == 0)
            throw new ApplicationException("Model is not entered.");

        if (EngineId == 0)
            throw new ApplicationException("Engine is not entered.");

        if (OwnerId == 0)
            throw new ApplicationException("Owner is not entered.");

        try
        {
            await _regService.InsertRegistrationAsync(r, ModelId, EngineId, OwnerId);
            return Ok(string.Format("Registration inserted. {0}", r.RegistrationNumber));
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteRegistrationAsync(int id)
    {
        if (id == 0)
            throw new ApplicationException("Reginstration is not found");

        try
        {
            await _regService.DeleteRegistrationAsync(id);
            return Ok("Registration is deleted");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateRegistrationAsync(int id, [FromBody] VehicleRegistrationWriteDTO UpdRegistration)
    {
        if (id == 0)
            throw new ApplicationException("Registration is not found");

        try
        {
            await _regService.UpdateRegistrationAsync(id, UpdRegistration);
            return Ok(string.Format("Registration data updated.\r\nNew Registration noumber {0}", UpdRegistration.RegistrationNumber));
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }
}
