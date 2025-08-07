using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.DTO;

namespace VehicleAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleOwnerController : ControllerBase
{
    private readonly VehicleDbContext _context;

    public VehicleOwnerController(VehicleDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleOwnerDTO>>> GetOwners()
    {
        var owner = await _context.VehicleOwner
            .Include(vr => vr.VehicleRegistrations)
            .Select(o => new VehicleOwnerDTO { LastName = o.LastName, FirstName = o.FirstName, DOB = o.DOB })
            .ToListAsync();

        return Ok(owner);
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<IEnumerable<VehicleOwnerDTO>>> GetOwnerById(string name)
    {
        var owner = await _context.VehicleOwner
            .Where(o => o.FirstName == name || o.LastName == name)
            .Include(vr => vr.VehicleRegistrations)
            .Select(o => new VehicleOwnerDTO { LastName = o.LastName, FirstName = o.FirstName, DOB = o.DOB })
            .ToListAsync();

        return Ok(owner);
    }

    [HttpPost]
    public async Task<ActionResult> InsOwner([FromBody] VehicleOwnerDTO o)
    {
        if (o == null)
            return BadRequest("Owner is not entered.");

        var owner = new VehicleOwner()
        {
            LastName = o.LastName,
            FirstName = o.FirstName,
            DOB = o.DOB
        };

        _context.VehicleOwner.Add(owner);
        await _context.SaveChangesAsync();

        return Ok(string.Format("Owner inserted.\r\n{0} {1} {2}", o.FirstName, o.LastName, o.DOB));
    }

    [HttpDelete]
    public async Task<ActionResult> DelOwner(int id)
    {
        var o = await _context.VehicleOwner.FindAsync(id);

        if (o == null)
            return NotFound();

        _context.VehicleOwner.Remove(o);
        await _context.SaveChangesAsync();

        return Ok(string.Format("Owner {0} {1} {2} deleted", o.Id, o.FirstName, o.LastName));
    }

    [HttpPut]
    public async Task<ActionResult> UpdOwner(int id, [FromBody] VehicleOwnerDTO UpdOwner)
    {
        var o = await _context.VehicleOwner.FindAsync(id);
        if (o == null)
            return NotFound();

        o.FirstName= UpdOwner.FirstName;
        o.LastName= UpdOwner.LastName;
        o.DOB= UpdOwner.DOB;

        await _context.SaveChangesAsync();

        return Ok(string.Format("Owner data updated.\r\n" +
            "New data: {0}\t{1}",  
            UpdOwner.FirstName, UpdOwner.LastName));
    }
}
