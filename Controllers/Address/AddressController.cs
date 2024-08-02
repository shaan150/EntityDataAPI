using EntityDataAPI.DTOs;
using EntityDataAPI.Exceptions.CRUD;
using EntityDataAPI.Exceptions.Operations;
using EntityDataAPI.Repos.Entity;
using EntityDataAPI.Utils;
using EntityDataAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using EntityDataAPI.Enums;

namespace EntityDataAPI.Controllers.Address;
using EntityDataAPI.Models.Entity;

[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly IAddressRepo _addressRepo;
    private readonly IEntityRepo _entityRepo;

    public AddressController(IAddressRepo addressRepo, IEntityRepo entityRepo)
    {
        _addressRepo = addressRepo;
        _entityRepo = entityRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] AddressFilter filter)
    {
        try
        {
            var addressDTOs = await AddressControllerReceiver.GetAddresses(_addressRepo, filter);
            return Ok(addressDTOs);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            var addressDTO = await AddressControllerReceiver.GetAddress(_addressRepo, id);
            return Ok(addressDTO);
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddressCreateDTO addressCreateDTO)
    {
        try
        {
            string id = null;
            await RetryHelper.RetryOnExceptionAsync<OperationException>(async () =>
            {
                id = await AddressControllerCreator.CreateAddress(_entityRepo, _addressRepo, addressCreateDTO);
            });
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
        catch (OperationException e)
        {
            return BadRequest(e.Message);
        }
    }
}
