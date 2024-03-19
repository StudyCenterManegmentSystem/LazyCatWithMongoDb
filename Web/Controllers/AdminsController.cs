using Application.Commens.Helpers;
using Application.Message;

namespace Web.Controllers;

[Route("api/admins")]
[ApiController]
public class AdminsController(IAdminService adminService, SendService emailService) : ControllerBase
{
    private readonly IAdminService _adminService = adminService;
    private readonly SendService _emailService = emailService;

    [HttpPost("register-teacher")]
    //[Authorize(Roles = "SuperAdmin, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterTeacher(TeacherRegisterRequest request)
    {
        try
        {
            var response = await _adminService.RegisterTeacherAsync(request);
            await _emailService.SendEmail(request.Email, request.Password);
            return response.Success ? Ok(response) : Conflict(response);
        }
        catch (InvalidDataException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return BadRequest(ex.Message);
        }
        catch (CustomException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return BadRequest(ex.Message);
        }
        catch (ValidationException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);
            return BadRequest(ex.Message);
        }

        catch (NotFoundException ex)
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

    [HttpPost("register-admin")]
    //[Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterAdmin(RegistrationRequest request)
    {
        try
        {
            var response = await _adminService.RegisterAdminAsync(request);
            return response.Success ? Ok(response) : Conflict(response);
        }
        catch (CustomException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return BadRequest(ex.Message);
        }
        catch (ValidationException ex)
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

    [HttpPost("register-superadmin")]
    //[Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterSuperAdmin(RegistrationRequest request)
    {
        try
        {
            var response = await _adminService.RegisterSuperAdminAsync(request);
            return response.Success ? Ok(response) : Conflict(response);
        }
        catch (CustomException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return BadRequest(ex.Message);
        }
        catch (ValidationException ex)
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
    //[Authorize(Roles = "SuperAdmin, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAccount(TeacherLoginRequest request)
    {
        try
        {
            await _adminService.DeleteAccountAsync(request);
            return Ok("Account deleted successfully");
        }
        catch (NotFoundException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return NotFound(ex.Message);
        }
        catch (ValidationException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return BadRequest(ex.Message);
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

    [HttpDelete("delete-admin-account")]
    //[Authorize(Roles = "SuperAdmin , Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAdminAccount(LoginRequest request)
    {
        try
        {
            await _adminService.DeleteAccountAsync(request);
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
        catch (ValidationException ex)
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

    [HttpDelete("delete-superadmin-account")]
    //[Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteSuperAdminAccount(TeacherLoginRequest request)
    {
        try
        {
            await _adminService.DeleteAccountAsync(request);
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
        catch (ValidationException ex)
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


    [HttpGet("all-teachers-with-fans")]
    //[Authorize(Roles = "SuperAdmin , Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TeacherWithFansRequest>>> GetAllTeachersWithFans()
    {
        try
        {
            var teachersWithFans = await _adminService.GetAllTeachersWithFanAsync();
            return Ok(teachersWithFans);
        }
        catch (NotFoundException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Error);

            return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message} => An error occurred while fetching teachers with fans.");
        }

    }


    [HttpGet("get-by-id-teacher-with-fans/{id}")]
    //[Authorize(Roles = "SuperAdmin , Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TeacherWithFansRequest>>> GetAllTeachersWithFans(string id)
    {
        try
        {
            var teachersWithFans = await _adminService.GetAllByIdTeacherWithFanAsync(id);
            return Ok(teachersWithFans);
        }
        catch (NotFoundException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Error);

            return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message} => An error occurred while fetching teachers with fans.");
        }
    }



}
