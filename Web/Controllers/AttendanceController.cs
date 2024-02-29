using Application.Commens.Helpers;
using Application.Dtos.AttendanceDtos;

namespace Web.Controllers;

[Route("api/attendances")]
[ApiController]
public class AttendanceController(IAttendanceService attendanceService) : ControllerBase
{
    private readonly IAttendanceService _attendanceService = attendanceService;

    [HttpPost("create-attendance")]
    //[Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add(AddAttendanceDto dto)
    {
        try
        {
            var result = await _attendanceService.AddAttendanceAsync(dto);
            return Ok($"{result} mofaqiyatli qo'shildi");
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

    [HttpPut("update-attendace")]
    //[Authorize(Roles = "SuperAdmin, Admin , Teacher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(UpdateAttendanceDto dto)
    {
        try
        {
            var result = await _attendanceService.UpdateAttendanceAsync(dto);
            return Ok($"{result} mofaqiyatli yangilandi");
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

    [HttpDelete("delete-attendace/{id}")]
    //[Authorize(Roles = "SuperAdmin, Admin , Teacher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var result = await _attendanceService.DeleteAttendanceAsync(id);
            return Ok($"{result} mofaqiyatli o'chirildi");
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

    [HttpGet("getall-attendace")]
    //[Authorize(Roles = "SuperAdmin, Admin , Teacher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAttendance()
    {
        try
        {
            var result = await _attendanceService.GetAllAttendanceAsync();
            return Ok(result);
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

    [HttpGet("gett-by-id-attendace")]
    //[Authorize(Roles = "SuperAdmin, Admin , Teacher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAttendance(string id)
    {
        try
        {
            var result = await _attendanceService.GetByIdAttendanceAsync(id);
            return Ok(result);
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
}
