using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.DTO;

namespace VehicleAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleMakeController : ControllerBase
{
    private readonly VehicleDbContext _context;

    public VehicleMakeController(VehicleDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleMakeDTO>>> GetMake()
    {
        var make = await _context.VehicleMake
            .Include(vm => vm.VehicleModels)
            .Select(m => new VehicleMakeDTO { Name = m.Name, Abrv = m.Abrv })
            .ToListAsync();

        return Ok(make);
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<IEnumerable<VehicleMakeDTO>>> GetMakeById(string name)
    {
        var make = await _context.VehicleMake
            .Where(m => m.Name == name || m.Abrv == name)
            .Include(vm => vm.VehicleModels)
            .Select(m => new VehicleMakeDTO { Name = m.Name, Abrv = m.Abrv })
            .ToListAsync();

        return Ok(make);
    }

    [HttpPost]
    public async Task<ActionResult> InsMake([FromBody] VehicleMakeDTO m)
    {
        if (m == null)
            return BadRequest("Make is not entered.");

        var make = new VehicleMake()
        {
            Name = m.Name,
            Abrv = m.Abrv
        };

        _context.VehicleMake.Add(make);
        await _context.SaveChangesAsync();

        return Ok(string.Format("Make inserted. {0}", m.Name));
    }

    [HttpDelete]
    public async Task<ActionResult> DelMake(int id)
    {
        var m = await _context.VehicleMake.FindAsync(id);

        if (m == null)
            return NotFound();

        _context.VehicleMake.Remove(m);
        await _context.SaveChangesAsync();

        return Ok(string.Format("Make {0} is deleted", m.Name));
    }

    [HttpPut]
    public async Task<ActionResult> UpdMake(int id, [FromBody] VehicleMakeDTO UpdMake)
    {
        var m = await _context.VehicleMake.FindAsync(id);
        if (m == null)
            return NotFound();

        m.Name = UpdMake.Name;
        m.Abrv = UpdMake.Abrv;
        await _context.SaveChangesAsync();

        return Ok(string.Format("Make data updated.\r\nNew make name {0}", UpdMake.Name));
    }
}
