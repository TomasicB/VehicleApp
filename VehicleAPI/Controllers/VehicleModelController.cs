using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;

namespace VehicleAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleModelController : ControllerBase
{
    private readonly VehicleDbContext _context;

    public VehicleModelController(VehicleDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleModelDTO>>> GetModels()
    {
        var model = await _context.VehicleModel
            .ToListAsync();

        return Ok(model);
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<IEnumerable<VehicleModelDTO>>> GetModelById(string name)
    {
        var model = await _context.VehicleModel
            .Where(m => m.Name == name || m.Abrv == name)
            .ToListAsync();

        return Ok(model);
    }

    [HttpPost]
    public async Task<ActionResult> InsModel([FromBody] VehicleModelDTO model, int makeid)
    {
        if (model == null)
            return BadRequest("Model is not entered.");

        var make = await _context.VehicleMake.FindAsync(makeid);
        if (make == null)
            return BadRequest("Make is not entered.");

        model.VehicleMakeId = make.Id;

        _context.VehicleModel.Add(model);
        await _context.SaveChangesAsync();

        return Ok(string.Format("Model inserted. ({1}){0}", model.Name, make.Name));
    }

    [HttpDelete]
    public async Task<ActionResult> DelModel(int id)
    {
        var m = await _context.VehicleModel.FindAsync(id);

        if (m == null)
            return NotFound();

        _context.VehicleModel.Remove(m);
        await _context.SaveChangesAsync();

        return Ok(string.Format("Model {0} is deleted", m.Name));
    }

    [HttpPut]
    public async Task<ActionResult> UpdModel(int id, [FromBody] VehicleModelDTO UpdModel)
    {
        if (id != UpdModel.Id)
            return BadRequest("Model ID mismatch");

        var m = await _context.VehicleModel.FindAsync(id);
        if (m == null)
            return NotFound();

        m.Name = UpdModel.Name;
        m.VehicleMakeId = UpdModel.VehicleMakeId;
        await _context.SaveChangesAsync();

        return Ok(string.Format("Model data updated.\r\nNew model name {0}", UpdModel.Name));
    }
}
