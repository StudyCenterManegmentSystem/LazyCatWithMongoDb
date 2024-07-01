using Application.Commens.Constants;
using Application.Commens.Helpers;

namespace Web.Controllers;
[ApiController]
[Route("api/fans")]
//[Authorize(Roles = IdentityRoles.TEACHER)]
public class FansController(IFanService fanService) : ControllerBase
{
    private readonly IFanService _fanService = fanService;

    [HttpGet("get-all-fans")]
    //[Authorize(Roles = "SuperAdmin, Admin , Teacher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var fans = await _fanService.GetAllAsync();
            return Ok(fans);
        }
        catch (CustomException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return BadRequest(ex.Message);
        }
        catch(NotFoundException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return NoContent();
        }
        catch (Exception ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Error);

            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex.Message}");
        }
    }

    [HttpGet("get-by-id-fan/{id}")]
    //[Authorize(Roles = "SuperAdmin, Admin, Teacher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            var fan = await _fanService.GetByIdAsync(id);
            return Ok(fan);
        }
        catch (CustomException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return BadRequest(ex.Message);
        }
        catch (NotFoundException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return BadRequest(ex.Message);
        }
        
        catch (Exception ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Error);

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [HttpPost("create-fan")]
 //   [Authorize(Roles = "SuperAdmin, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add(AddFanDto addFan)
    {
        try
        {
            await _fanService.AddAsync(addFan);
            return Ok("Added");
        }
        catch (CustomException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return BadRequest(ex.Message);
        }
        catch(ArgumentNullException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Error);

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("update-fan")]
  //  [Authorize(Roles = "SuperAdmin, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(FanDto fanDto)
    {
        try
        {
            await _fanService.UpdateAsync(fanDto);
            return Ok("Updated");
        }
        catch (CustomException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return BadRequest(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Error);

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("delete-fan/{id}")]
   // [Authorize(Roles = "SuperAdmin, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await _fanService.DeleteAsync(id);
            return Ok("Deleted");
        }

        catch (CustomException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return BadRequest(ex.Message);
        }

        catch(NotFoundException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return BadRequest(ex.Message);
        }

        catch (Exception ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Error);

            return StatusCode(StatusCodes.Status500InternalServerError ,ex.Message);
        }
    }


    [HttpGet("get-all-with-teachers")]
   // [Authorize(Roles = "SuperAdmin, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllWithTeachers()
    {
        try
        {
            var fans = await _fanService.GetAllFanTeachers();
            return Ok(fans);
        }
        catch (CustomException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return BadRequest(ex.Message);
        }

        catch (Exception ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Error);

            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex.Message}");
        }
    }


    [HttpGet("get-by-id-fan-with-teachers/{id}")]
   // [Authorize(Roles = "SuperAdmin, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdFanWithTeachers(string id)
    {
        try
        {
            var fans = await _fanService.GetByIdFanWithTeachers(id);
            return Ok(fans);
        }
        catch (CustomException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return BadRequest(ex.Message);
        }
        catch(NotFoundException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Error);

            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex.Message}");
        }
    }
}