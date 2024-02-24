using Application.Dtos.PaymentDtos;

namespace Web.Controllers;

[Route("api/payments")]
[ApiController]
public class PaymentController(IPaymentService paymentService) : ControllerBase
{
    private readonly IPaymentService _paymentService = paymentService;

    [HttpPost("create-payment")]  
    public async Task<IActionResult> AddPayment(AddPaymentDto dto)
    {
        try
        {
            var payment = await _paymentService.AddPaymentAsync(dto);
            return Ok(payment);
        }
        catch(ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("update-payment")]
    public async Task<IActionResult> UpdatePayment(UpdatePaymentDto dto)
    {
        try
        {
            var payment = await _paymentService.UpdatePaymentAsync(dto);
            return Ok(payment);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [HttpDelete("delete-payment/{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        try
        {
            var payment = await _paymentService.DeletePaymentAsync(id);
            return Ok(payment);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("get-all-payments")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var result = await _paymentService.GetAllPayments();
            return Ok(result);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
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
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("get-by-id-payment/{id}")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        try
        {
            var result = await _paymentService.GetByIdAsync(id);
            return Ok(result);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
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
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
