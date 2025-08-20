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
    public async Task<ActionResult<IEnumerable<IVehicleModel>>> GetModelsAsync()
    {
        try
        {
            var model = await _modelService.GetModelsAsync();
            return Ok(model);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpGet("byId")]
    public async Task<ActionResult<IEnumerable<IVehicleModel>>> GetModelByIdAsync(int id)
    {
        try
        {
            var model = await _modelService.GetModelByIdAsync(id);
            return Ok(model);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpGet("byName")]
    public async Task<ActionResult<IEnumerable<IVehicleModel>>> GetModelsByNameAsync(string name)
    {
        try
        {
            var model = await _modelService.GetModelsByNameAsync(name);
            return Ok(model);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpGet("byMake")]
    public async Task<ActionResult<IEnumerable<IVehicleModel>>> GetModelsByMakeAsync(string make)
    {
        try
        {
            var model = await _modelService.GetModelsByMakeAsync(make);
            return Ok(model);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpPost]
    public async Task<ActionResult> InsertModelAsync([FromBody] VehicleModelDTO m, int makeid)
    {
        if (m == null)

            throw new ApplicationException("Model is not entered.");

        if (makeid == 0)
            throw new ApplicationException("Make is not entered.");

        try
        {
            await _modelService.InsertModelAsync(m, makeid);
            return Ok(string.Format("Model inserted. ({1}){0}", m.Name, m.Name));
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteModelAsync(int id)
    {
        if (id == 0)
            throw new ApplicationException("Model is not found");

        try
        {
            await _modelService.DeleteModelAsync(id);
            return Ok("Model is deleted");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateModelAsync(int id, [FromBody] VehicleModelWriteDTO UpdModel)
    {
        if (id == 0)
            throw new ApplicationException("Model is not found");

        try
        {
            await _modelService.UpdateModelAsync(id, UpdModel);
            return Ok(string.Format("Model data updated.\r\nNew model name {0}", UpdModel.Name));
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong", ex);
        }
    }
}
