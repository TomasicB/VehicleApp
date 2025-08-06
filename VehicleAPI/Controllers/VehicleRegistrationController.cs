using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;

namespace VehicleAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleRegistrationController : ControllerBase
{
    private readonly VehicleDbContext _context;

    public VehicleRegistrationController(VehicleDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleRegistrationDTO>>> GetRegistrations()
    {
        var registration = await _context.VehicleRegistration
            .ToListAsync();

        return Ok(registration);
    }

    [HttpGet("{number}")]
    public async Task<ActionResult<IEnumerable<VehicleRegistrationDTO>>> GetRegistrationById(string number)
    {
        var registration = await _context.VehicleRegistration
            .Where(r => r.RegistrationNumber == number || r.RegistrationNumber == number)
            .ToListAsync();

        return Ok(registration);
    }

    [HttpPost]
    public async Task<ActionResult> InsRegistration([FromBody] VehicleRegistrationDTO registration, int ModelId, int EngineId, int OwnerId)
    {
        if (registration == null)
            return BadRequest("Registration is not entered.");

        var model = await _context.VehicleModel.FindAsync(ModelId);
        if (model == null)
            return BadRequest("Model is not entered.");

        var engine = await _context.VehicleEngine.FindAsync(EngineId);
        if (engine == null)
            return BadRequest("Engine is not entered.");

        var owner = await _context.VehicleOwner.FindAsync(OwnerId);
        if (owner == null)
            return BadRequest("Owner is not entered.");

        registration.VehicleModelId = model.Id;
        registration.VehicleEngineId = engine.Id;
        registration.VehicleOwnerId = owner.Id;

        _context.VehicleRegistration.Add(registration);
        await _context.SaveChangesAsync();

        return Ok(string.Format("Registration inserted. {0}\t{1} {2}\t{3},{4}", registration.RegistrationNumber, owner.FirstName, owner.LastName, engine.Type, model.Name));
    }

    [HttpDelete]
    public async Task<ActionResult> DelRegistration(int id)
    {
        var r = await _context.VehicleRegistration.FindAsync(id);

        if (r == null)
            return NotFound();

        _context.VehicleRegistration.Remove(r);
        await _context.SaveChangesAsync();

        return Ok(string.Format("Registration {0} is deleted", r.RegistrationNumber));
    }

    [HttpPut]
    public async Task<ActionResult> UpdRegistration(int id, [FromBody] VehicleRegistrationDTO UpdRegistration)
    {
        if (id != UpdRegistration.Id)
            return BadRequest("Registration ID mismatch");

        var r = await _context.VehicleRegistration.FindAsync(id);
        if (r == null)
            return NotFound();

        r.RegistrationNumber = UpdRegistration.RegistrationNumber;
        r.VehicleEngineId = UpdRegistration.VehicleEngineId;
        r.VehicleModelId = UpdRegistration.VehicleModelId;
        r.VehicleOwnerId = UpdRegistration.VehicleOwnerId;
        await _context.SaveChangesAsync();

        return Ok(string.Format("Registration data updated.\r\nNew Registration name {0}", UpdRegistration.RegistrationNumber));
    }
}
