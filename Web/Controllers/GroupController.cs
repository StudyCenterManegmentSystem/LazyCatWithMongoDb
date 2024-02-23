

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
                return  BadRequest(ex.Message);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
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
                return BadRequest(ex.Message);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
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
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
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
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex.Message}");
            }
        }
    }
}
