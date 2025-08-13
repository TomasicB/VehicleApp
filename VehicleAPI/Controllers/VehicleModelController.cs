using Microsoft.AspNetCore.Mvc;
using Vehicle.Models.Common;
using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;
using Vehicle.Service.Common;

namespace Vehicle.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleModelController : ControllerBase
{
    private readonly IVehicleModelService _modelService;

    public VehicleModelController(IVehicleModelService modelService)
    {
        _modelService = modelService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IVehicleModel>>> GetModels()
    {
        try
        {
            var model = await _modelService.GetModels();
            return Ok(model);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<IEnumerable<IVehicleModel>>> GetModelById(int id)
    {
        try
        {
            var model = await _modelService.GetModelById(id);
            return Ok(model);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("{name:string}")]
    public async Task<ActionResult<IEnumerable<IVehicleModel>>> GetModelByName(string name)
    {
        try
        {
            var model = await _modelService.GetModelByName(name);
            return Ok(model);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPost]
    public async Task<ActionResult> InsModel([FromBody] VehicleModelDTO m, int makeid)
    {
        if (m == null)
            return BadRequest("Model is not entered.");

        if (makeid == 0)
            return BadRequest("Make is not entered.");

        try
        {
            await _modelService.InsModel(m, makeid);
            return Ok(string.Format("Model inserted. ({1}){0}", m.Name, m.Name));
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DelModel(int id)
    {
        if (id == 0)
            return NotFound();

        try
        {
            await _modelService.DelModel(id);
            return Ok("Model is deleted");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdModel(int id, [FromBody] VehicleModelWriteDTO UpdModel)
    {
        if (id == 0)
            return NotFound();

        try
        {
            await _modelService.UpdModel(id, UpdModel);
            return Ok(string.Format("Model data updated.\r\nNew model name {0}", UpdModel.Name));
        }
        catch (Exception)
        {
            throw;
        }
    }
}
