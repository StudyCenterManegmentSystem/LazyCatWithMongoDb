

using Application.Commens.Helpers;
using Application.Dtos.GroupsDto;

namespace Web.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupController(IGuruhService gruopInterface) : ControllerBase
    {
        private readonly IGuruhService _gruopInterface = gruopInterface;

        [HttpPost("create-guruh")]
        public async Task<IActionResult> CreateGuruh(AddGroupDto dto)
        {
            try
            {
                var result =   await _gruopInterface.AddGroupAsync(dto);
                return Ok(result);
            }
            catch(ArgumentNullException ex)
            {
                _ = LoggerBot.Log(ex.Message, LogType.Warning);

                return BadRequest(ex.Message);
            }
            catch (CustomException ex)
            {
                _ = LoggerBot.Log(ex.Message, LogType.Warning);

                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
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

        [HttpPut("update-guruh")]
        public async Task<IActionResult> UpdateGuruh(UpdateGroupDto dto)
        {
            try
            {
               var result =   await _gruopInterface.UpdateAsync(dto);
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                _ = LoggerBot.Log(ex.Message, LogType.Warning);

                return BadRequest(ex.Message);
            }
            catch (CustomException ex)
            {
                _ = LoggerBot.Log(ex.Message, LogType.Warning);

                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
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

        [HttpGet("get-all-guruh")]

        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _gruopInterface.GetAllGuruhAsync();
                return Ok(result);
            }
            catch(NotFoundException ex)
            {
                _ = LoggerBot.Log(ex.Message, LogType.Warning);

                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                _ = LoggerBot.Log(ex.Message, LogType.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex.Message}");
            }
        }

        [HttpGet("get-by-id-guruh/{id}")]

        public async Task<IActionResult> GetByIdAsync(string id)
        {
            try
            {
                var result = await _gruopInterface.GetByIdAsync(id);
                return Ok(result);
            }
            catch (NotFoundException ex)
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
        [HttpDelete("delete-guruh/{id}")]

        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                 await _gruopInterface.DeleteAsync(id);
                return Ok("Guruh mofaqiyatli o'chirildi");
            }
            catch (NotFoundException ex)
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
}
