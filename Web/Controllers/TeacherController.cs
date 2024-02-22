using Application.Dtos.TeacherDto;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeacherController(ITeacherService teacherService) : ControllerBase
{
    private readonly ITeacherService _teacherService = teacherService;

    [HttpPost("login-teacher")]
    public async Task<IActionResult> Login(TeacherLoginRequest request)
    {
        try
        {
            var response = await _teacherService.LoginAsync(request);
            return Ok(response);
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

    [HttpPatch("change-teacher-password")]
    public async Task<IActionResult> ChangePassword(TeacherChangePasswordRequest request)
    {
        try
        {
            var response = await _teacherService.ChangePasswordAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
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


    [HttpPatch("logout-teacher")]
    public async Task<IActionResult> Logout(TeacherLoginRequest request)
    {
        try
        {
            await _teacherService.LogoutAsync(request);
            return Ok("Logout successful");
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
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

    [HttpDelete("delete-teacher-account")]
    public async Task<IActionResult> DeleteAccount(TeacherLoginRequest request)
    {
        try
        {
            await _teacherService.DeleteAccountAsync(request);
            return Ok("Account deleted successfully");
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
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

    [HttpGet("all-with-fans")]
    public async Task<ActionResult<IEnumerable<TeacherWithFansRequest>>> GetAllTeachersWithFans()
    {
        try
        {
            var teachersWithFans = await _teacherService.GetAllTeachersWithFanAsync();
            return Ok(teachersWithFans);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while fetching teachers with fans.");
        }

    }


    [HttpGet("get-by-id-teacher/{id}")]
    public async Task<ActionResult<IEnumerable<TeacherWithFansRequest>>> GetAllTeachersWithFans(string id)
    {
        try
        {
            var teachersWithFans = await _teacherService.GetAllByIdTeacherWithFanAsync(id);
            return Ok(teachersWithFans);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while fetching teachers with fans.");
        }

    }
}
