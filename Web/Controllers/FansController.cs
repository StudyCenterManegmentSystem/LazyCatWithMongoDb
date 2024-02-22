

using Application.Commens.Constants;
using Application.Dtos.FanDtos;

namespace Web.Controllers;
[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = IdentityRoles.TEACHER)]
public class FansController(IFanService fanService) : ControllerBase
{
    private readonly IFanService _fanService = fanService;

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var fans = await _fanService.GetAllAsync();
            return Ok(fans);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            var fan = await _fanService.GetByIdAsync(id);
            return Ok(fan);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("AddFan")]
    public async Task<IActionResult> Add(AddFanDto addFan)
    {
        try
        {
            await _fanService.AddAsync(addFan);
            return Ok("Added");
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut("UpdateFan")]
    public async Task<IActionResult> Update(FanDto fanDto)
    {
        try
        {
            await _fanService.UpdateAsync(fanDto);
            return Ok("Updated");
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await _fanService.DeleteAsync(id);
            return Ok("Deleted");
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}