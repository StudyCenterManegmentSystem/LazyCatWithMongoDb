



using Application.Commens.Helpers;

namespace Application.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController(IIdentityService identityService) : ControllerBase
{
    private readonly IIdentityService _identityService = identityService;


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            var response = await _identityService.LoginAsync(request);
            return response.Success ? Ok(response) : Unauthorized(response);
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


    [HttpPatch("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        try
        {
            var response =  await _identityService.ChangePasswordAsync(request);
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


    [HttpPatch("logout")]
    public async Task<IActionResult> Logout(LoginRequest request)
    {
        try
        {
            await _identityService.LogoutAsync(request);
            return NoContent();
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

    [HttpDelete("delete-account")]
    public async Task<IActionResult> DeleteAccount(LoginRequest request)
    {
        try
        {
            await _identityService.DeleteAccountAsync(request);
            return NoContent();
        }
        catch (CustomException ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _ = LoggerBot.Log(ex.Message, LogType.Warning);

            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex.Message}");
        }
    }
}
