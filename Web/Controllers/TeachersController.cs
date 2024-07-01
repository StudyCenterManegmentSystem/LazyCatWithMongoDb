
using Application.Commens.Helpers;

namespace Web.Controllers;

[Route("api/teachers")]
[ApiController]
public class TeachersController(ITeacherService teacherService) : ControllerBase
{
    private readonly ITeacherService _teacherService = teacherService;

    [HttpPost("login-teacher")]
    //[Authorize(Roles = "SuperAdmin, Admin , Teacher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login(TeacherLoginRequest request)
    {
        try
        {
            var response = await _teacherService.LoginAsync(request);
            return Ok(response);
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

    [HttpPatch("change-teacher-password")]
    //[Authorize(Roles = "SuperAdmin, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangePassword(TeacherChangePasswordRequest request)
    {
        try
        {
            var response = await _teacherService.ChangePasswordAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
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


    [HttpPatch("logout-teacher")]
    //[Authorize(Roles = "SuperAdmin, Admin , Teacher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Logout(TeacherLoginRequest request)
    {
        try
        {
            await _teacherService.LogoutAsync(request);
            return Ok("Logout successful");
        }
        catch (NotFoundException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return NotFound(ex.Message);
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

    [HttpDelete("delete-teacher-account")]
    [Authorize(Roles = "SuperAdmin, Admin ")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAccount(TeacherLoginRequest request)
    {
        try
        {
            await _teacherService.DeleteAccountAsync(request);
            return Ok("Account deleted successfully");
        }
        catch (NotFoundException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return NotFound(ex.Message);
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

    [HttpGet("all-with-fans")]
    //[Authorize(Roles = "SuperAdmin, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TeacherWithFansRequest>>> GetAllTeachersWithFans()
    {
        try
        {
            var teachersWithFans = await _teacherService.GetAllTeachersWithFanAsync();
            return Ok(teachersWithFans);
        }
        catch (NotFoundException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return NotFound(ex.Message);
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


    [HttpGet("get-by-id-teacher/{id}")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TeacherWithFansRequest>>> GetAllTeachersWithFans(string id)
    {
        try
        {
            var teachersWithFans = await _teacherService.GetAllByIdTeacherWithFanAsync(id);
            return Ok(teachersWithFans);
        }
        catch (NotFoundException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return NotFound(ex.Message);
        }
        catch(CustomException ex)
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
}
