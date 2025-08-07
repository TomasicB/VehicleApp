using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.DTO;

namespace VehicleAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleEngineController : ControllerBase
{
    private readonly VehicleDbContext _context;

    public VehicleEngineController(VehicleDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleEngineDTO>>> GetEngine()
    {
        var engine = await _context.VehicleEngine
            .Include(vr => vr.VehicleRegistrations)
            .Select(e => new VehicleEngineDTO { Type = e.Type, Abrv = e.Abrv })
            .ToListAsync();

        return Ok(engine);
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<IEnumerable<VehicleEngineDTO>>> GetEngineById(string type)
    {
        var engine = await _context.VehicleEngine
            .Where(e => e.Type == type || e.Abrv == type)
            .Include(vr => vr.VehicleRegistrations)
            .Select(e => new VehicleEngineDTO { Type = e.Type, Abrv = e.Abrv })
            .ToListAsync();

        return Ok(engine);
    }

    [HttpPost]
    public async Task<ActionResult> InsEngine([FromBody] VehicleEngineDTO e)
    {
        if (e == null)
            return BadRequest("Engine is not entered.");

        var engine = new VehicleEngine()
        {
            Type = e.Type,
            Abrv = e.Abrv
        };

        _context.VehicleEngine.Add(engine);
        await _context.SaveChangesAsync();

        return Ok(string.Format("Engine inserted. {0}", engine.Type));
    }

    [HttpDelete]
    public async Task<ActionResult> DelEngine(int id)
    {
        var e = await _context.VehicleEngine.FindAsync(id);

        if (e == null)
            return NotFound();

        _context.VehicleEngine.Remove(e);
        await _context.SaveChangesAsync();

        return Ok(string.Format("Engine {0} is deleted", e.Type));
    }

    [HttpPut]
    public async Task<ActionResult> UpdEngine(int id, [FromBody] VehicleEngineDTO UpdEngine)
    {
        var e = await _context.VehicleEngine.FindAsync(id);
        if (e == null)
            return NotFound();

        e.Type = UpdEngine.Type;
        e.Abrv = UpdEngine.Abrv;
        await _context.SaveChangesAsync();

        return Ok(string.Format("Engine data updated.\r\nNew Engine name {0}", UpdEngine.Type));
    }
}
