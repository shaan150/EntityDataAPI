using EntityDataAPI.DTOs;
using EntityDataAPI.Exceptions.Operations;
using EntityDataAPI.Exceptions.CRUD;
using EntityDataAPI.Repos.Entity;
using EntityDataAPI.Utils.DTOMappers;
using Microsoft.AspNetCore.Mvc;
using EntityDataAPI.Filters;
using EntityDataAPI.Utils;

namespace EntityDataAPI.Controllers.Entity;

[Route("api/[controller]")]
[ApiController]
public class EntityController : ControllerBase
{

    private readonly IEntityRepo _entityRepo;

    public EntityController(IEntityRepo entityRepo)
    {
        _entityRepo = entityRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] EntityFilter filter)
    {
        try
        {
            var entityDTOs = await EntityControllerReceiver.GetEntities(_entityRepo, filter);
            return Ok(entityDTOs);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        try {             
            var entityDTO = await EntityControllerReceiver.GetEntity(_entityRepo, id);
            return Ok(entityDTO);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchFilter search)
    {
        try {
            if (search.GetDetails)
            {
                var entityDTOs = await EntityControllerReceiver.SearchEntities(_entityRepo, search);
                return Ok(entityDTOs);
            }
            else
            {
                var entityIds = await EntityControllerReceiver.SearchEntityIds(_entityRepo, search);
                return Ok(entityIds);
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EntityCreateDTO entityCreateDTO)
    {
        try
        {
            string id = null;
            await RetryHelper.RetryOnExceptionAsync<OperationException>(async () =>
            {
                id = await EntityControllerCreator.CreateEntity(_entityRepo, entityCreateDTO);
            });
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
        catch (OperationException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] EntityUpdateDTO entityUpdateDTO)
    {
        try
        {
            await RetryHelper.RetryOnExceptionAsync<OperationException>(async () =>
            {
                await EntityControllerUpdater.UpdateEntity(_entityRepo, id, entityUpdateDTO);
            });
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (OperationException e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try {
            await EntityControllerDeleter.DeleteEntity(id, _entityRepo);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (OperationException e)
        {
            return BadRequest(e.Message);
        }
    }


}
