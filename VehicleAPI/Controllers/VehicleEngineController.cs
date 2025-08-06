using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle.DAL;
using Vehicle.Models;

namespace VehicleAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleEngineController : ControllerBase
{
    private readonly VehicleDbContext _context;

    public VehicleEngineController(VehicleDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleEngine>>> GetEngine()
    {
        var engine = await _context.VehicleEngine
            .ToListAsync();

        return Ok(engine);
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<IEnumerable<VehicleEngine>>> GetEngineById(string type)
    {
        var engine = await _context.VehicleEngine
            .Where(e => e.Type == type || e.Abrv == type)
            .ToListAsync();

        return Ok(engine);
    }

    [HttpPost]
    public async Task<ActionResult> InsEngine([FromBody] VehicleEngine e)
    {
        if (e == null)
            return BadRequest("Engine is not entered.");

        _context.VehicleEngine.Add(e);
        await _context.SaveChangesAsync();

        return Ok(string.Format("Engine inserted. {0}", e.Type));
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
    public async Task<ActionResult> UpdEngine(int id, [FromBody] VehicleEngine UpdEngine)
    {
        if (id != UpdEngine.Id)
            return BadRequest("Engine ID mismatch");

        var e = await _context.VehicleEngine.FindAsync(id);
        if (e == null)
            return NotFound();

        e.Type = UpdEngine.Type;
        await _context.SaveChangesAsync();

        return Ok(string.Format("Engine data updated.\r\nNew Engine name {0}", UpdEngine.Type));
    }
}
