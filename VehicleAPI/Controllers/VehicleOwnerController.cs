using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Vehicle.DAL;
using Vehicle.Models;

namespace VehicleAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleOwnerController : ControllerBase
{
    private readonly VehicleDbContext _context;

    public VehicleOwnerController(VehicleDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleOwner>>> GetOwners()
    {
        var owner = await _context.VehicleOwner
            .ToListAsync();

        return Ok(owner);
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<IEnumerable<VehicleOwner>>> GetOwnerById(string name)
    {
        var owner = await _context.VehicleOwner
            .Where(o => o.FirstName == name || o.LastName == name)
            .ToListAsync();

        return Ok(owner);
    }

    [HttpPost]
    public async Task<ActionResult> InsOwner([FromBody] VehicleOwner o)
    {
        if (o == null)
        {
            return BadRequest("Owner is not entered.");
        }

        _context.VehicleOwner.Add(o);
        await _context.SaveChangesAsync();

        return Ok(string.Format("Owner inserted.\r\n{0} {1} {2}", o.FirstName, o.LastName, o.DOB));
    }

    [HttpDelete]
    public async Task<ActionResult> DelOwner(int id)
    {
        var o = await _context.VehicleOwner.FindAsync(id);

        if (o == null)
        {
            return NotFound();
        }

        _context.VehicleOwner.Remove(o);
        await _context.SaveChangesAsync();

        return Ok(string.Format("Owner {0} {1} {2} deleted", o.Id, o.FirstName, o.LastName));
    }

    [HttpPut]
    public async Task<ActionResult> UpdOwner(int id, [FromBody] VehicleOwner UpdOwner)
    {
        if (id != UpdOwner.Id)
        {
            return BadRequest("Owner ID mismatch");
        }

        var o = await _context.VehicleOwner.FindAsync(id);
        if (o == null)
        {
            return NotFound();
        }

        o.FirstName= UpdOwner.FirstName;
        o.LastName= UpdOwner.LastName;
        o.DOB= UpdOwner.DOB;

        await _context.SaveChangesAsync();

        return Ok(string.Format("Owner data updated.\r\n" +
            "New data: {3}\t{4}\t{5}",  
            UpdOwner.Id, UpdOwner.FirstName, UpdOwner.LastName));
    }

}
